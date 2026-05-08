using ShopEZ_OrderService.DTOs;
using ShopEZ_OrderService.HttpClients;
using ShopEZ_OrderService.Models;
using ShopEZ_OrderService.Repositories;

namespace ShopEZ_OrderService.Services
{
    public class OrderService:IOrderService
    {
        // OrderRepository to interact with Orders table using EF Core
        private readonly IOrderRepository _orderRepo;
        // ProductServiceClient to call ProductService REST API
        private readonly IProductServiceClient _productClient;
        // CartServiceClient to call CartService REST API
        private readonly ICartServiceClient _cartClient;
        private readonly ILogger<OrderService> _logger;

        // Constructor — Dependency Injection
        public OrderService(
            IOrderRepository orderRepo,
            IProductServiceClient productClient,
            ICartServiceClient cartClient,
            ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _productClient = productClient;
            _cartClient = cartClient;
            _logger = logger;
        }

        //  Approach 1 — Direct Order from Product (Buy Now) 

        public async Task<OrderResponseDto> CreateOrderAsync(int userId, OrderDto dto)
        {
            // Validate userId
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            // Validate cart is not empty
            if (dto.CartItems == null || dto.CartItems.Count == 0)
                throw new ArgumentException("Cart cannot be empty.");

            // Check for duplicate products in cart
            var duplicates = dto.CartItems
                .GroupBy(x => x.ProductId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicates.Any())
                throw new ArgumentException($"Duplicate products in cart: {string.Join(", ", duplicates)}. Combine quantities instead.");

            var orderItems = new List<OrderItem>();

            foreach (var item in dto.CartItems)
            {
                // Fetch product details from ProductService via HTTP
                var product = await _productClient.GetProductAsync(item.ProductId);

                // Check if enough stock is available
                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException(
                        $"Insufficient stock for '{product.Name}'. Available: {product.Stock}, Requested: {item.Quantity}");

                // Create order item with price at time of order
                orderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    // Store product name directly — avoids cross-service joins
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    // Store price at time of order — preserves historical price
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                });
            }

            // Reduce stock in ProductService for each item
            foreach (var item in dto.CartItems)
                await _productClient.ReduceStockAsync(item.ProductId, item.Quantity);

            // Create and save order
            return await SaveOrderAsync(userId, orderItems);
        }

        //  Approach 2 — Order from Cart (Checkout) 

        public async Task<OrderResponseDto> CreateOrderFromCartAsync(int userId, string token)
        {
            // Validate userId
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            // Fetch cart items from CartService using user token
            var cart = await _cartClient.GetCartAsync(token);

            // Validate cart is not empty
            if (cart.Items == null || !cart.Items.Any())
                throw new InvalidOperationException("Cart is empty. Please add products to cart first.");

            var orderItems = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                // Validate stock from ProductService — ensure stock is still available
                var product = await _productClient.GetProductAsync(item.ProductId);

                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException(
                        $"Insufficient stock for '{item.ProductName}'. Available: {product.Stock}, Requested: {item.Quantity}");

                // Create order item from cart item
                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    // Product name already stored in cart
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    // Price already stored in cart at time of adding
                    Price = item.Price,
                    ImageUrl = item.ImageUrl
                });
            }

            // Reduce stock in ProductService for each item
            foreach (var item in cart.Items)
                await _productClient.ReduceStockAsync(item.ProductId, item.Quantity);

            // Create and save order
            var order = await SaveOrderAsync(userId, orderItems);

            // Clear cart after order is placed successfully
            await _cartClient.ClearCartAsync(token);

            _logger.LogInformation("Order created from cart: {OrderId} for User: {UserId}", order.OrderId, userId);
            return order;
        }

        // Get all orders — called by Admin
        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            // Map each order to DTO
            return orders.Select(MapToDto);
        }

        // Get orders for logged in user — called by Customer
        public async Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapToDto);
        }

        // Get single order by ID
        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Order ID must be greater than 0.");

            var order = await _orderRepo.GetOrderByIdAsync(id);
            // Return null if not found — controller handles 404
            if (order == null) return null;

            return MapToDto(order);
        }

        // Cancel order — customer can cancel own order, admin can cancel any order
        public async Task<bool> CancelOrderAsync(int orderId, int userId, bool isAdmin)
        {
            // Validate order ID
            if (orderId <= 0)
                throw new ArgumentException("Order ID must be greater than 0.");

            // Fetch order from database
            var order = await _orderRepo.GetOrderByIdAsync(orderId);

            // Throw if order not found
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} was not found.");

            // Customer can only cancel their own order
            // Admin can cancel any order
            if (!isAdmin && order.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to cancel this order.");

            // Throw if order is already cancelled
            if (order.IsCancelled)
                throw new InvalidOperationException("Order is already cancelled.");

            // Cancel the order in repository
            return await _orderRepo.CancelOrderAsync(orderId);
        }

        //  Private — Save Order to Database 

        private async Task<OrderResponseDto> SaveOrderAsync(int userId, List<OrderItem> orderItems)
        {
            // Create the order
            var order = new Order
            {
                UserId = userId,
                // Set order date to UTC now
                OrderDate = DateTime.UtcNow,
                // Calculate total amount from all order items
                TotalAmount = orderItems.Sum(x => x.Price * x.Quantity),
                OrderItems = orderItems
            };

            // Save order to database
            var created = await _orderRepo.CreateOrderAsync(order);
            _logger.LogInformation("Order created: {OrderId} for User: {UserId}", created.OrderId, userId);

            return MapToDto(created);
        }

        // ─── Map Order entity to OrderResponseDto ─────────────────────────────

        private static OrderResponseDto MapToDto(Order o) => new()
        {
            OrderId = o.OrderId,
            UserId = o.UserId,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            // Shows if order is cancelled
            IsCancelled = o.IsCancelled,
            // Map each OrderItem to OrderItemResponseDto
            Items = o.OrderItems.Select(oi => new OrderItemResponseDto
            {
                ProductId = oi.ProductId,
                ProductName = oi.ProductName,
                Quantity = oi.Quantity,
                Price = oi.Price,
                ImageUrl = oi.ImageUrl
            }).ToList()
        };
    }
}
