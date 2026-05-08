using WebApplication17.DTOs;
using WebApplication17.Models;
using WebApplication17.Repositories;

namespace WebApplication17.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(int userId, OrderDto dto)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in dto.CartItems) // CartItems is now a List<OrderItemDto>
            {
                //  Validate Product Exists
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Product ID {item.ProductId} not found.");

                //  Validate Stock
                if (item.Quantity > product.Stock)
                    throw new InvalidOperationException($"Insufficient stock for {product.Name}. Available: {product.Stock}");

                // Create the OrderItem  
                orderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                // Reduce the stock in the databas
                product.Stock -= item.Quantity;
                await _productRepo.UpdateAsync(product);
            }

            // Create the Order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = orderItems.Sum(x => x.Price * x.Quantity),
                OrderItems = orderItems
            };

            var created = await _orderRepo.CreateOrderAsync(order);

            // Convert entity to DTO and return
            return MapToDto(created);
        }

        // Get All Orders (Admin) 
        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            // Convert each order to DTO
            return orders.Select(MapToDto);
        }

        // Get Customer/User Orders
        public async Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId)
        {
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);
            // Convert to DTO
            return orders.Select(MapToDto);
        }

        //GetOrderById
        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);

            if (order == null)
                return null;

            return MapToDto(order);
        }

        // Conversion of MAP Entity  to  DTO
        private static OrderResponseDto MapToDto(Order o)
        {
            return new OrderResponseDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,

                // Map each OrderItem to OrderItemResponseDto

                Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown Product",
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }
    }
}
