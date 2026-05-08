using ShopEZ_CartService.DTOs;
using ShopEZ_CartService.HttpClients;
using ShopEZ_CartService.Models;
using ShopEZ_CartService.Repositories;

namespace ShopEZ_CartService.Services
{
    public class CartService:ICartService
    {
        // CartRepository to interact with CartItems table using EF Core
        private readonly ICartRepository _cartRepo;
        // ProductServiceClient to call ProductService REST API
        private readonly IProductServiceClient _productClient;
        private readonly ILogger<CartService> _logger;

        // Constructor — Dependency Injection
        public CartService(
            ICartRepository cartRepo,
            IProductServiceClient productClient,
            ILogger<CartService> logger)
        {
            _cartRepo = cartRepo;
            _productClient = productClient;
            _logger = logger;
        }

        // Get all cart items for logged in user
        public async Task<CartSummaryDto> GetCartAsync(int userId)
        {
            // Validate userId
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            // Fetch cart items from repository
            var cartItems = await _cartRepo.GetCartByUserIdAsync(userId);

            // Map cart items to response DTOs
            var items = cartItems.Select(MapToDto).ToList();

            // Return cart summary with items, total price and total items
            return new CartSummaryDto
            {
                Items = items,
                // Calculate total price of all items
                TotalPrice = items.Sum(i => i.Subtotal),
                // Calculate total number of items
                TotalItems = items.Sum(i => i.Quantity)
            };
        }

        // Add product to cart — fetches product details from ProductService
        public async Task<CartItemResponseDto> AddToCartAsync(int userId, AddToCartDto dto)
        {
            // Validate userId
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            // Fetch product details from ProductService via HTTP
            // ProductName, Price, Stock, ImageUrl are all fetched automatically
            var product = await _productClient.GetProductAsync(dto.ProductId);

            // Stock validation — cannot add more than available stock
            if (dto.Quantity > product.Stock)
                throw new InvalidOperationException(
                    $"Only {product.Stock} items available in stock for '{product.Name}'.");

            // Check if product already exists in cart
            var existingItem = await _cartRepo.GetCartItemAsync(userId, dto.ProductId);

            if (existingItem != null)
            {
                // Product exists — check if increasing quantity exceeds stock
                var newQuantity = existingItem.Quantity + dto.Quantity;

                if (newQuantity > product.Stock)
                    throw new InvalidOperationException(
                        $"Cannot add more. Only {product.Stock} items available in stock for '{product.Name}'.");

                // Increase quantity of existing item
                existingItem.Quantity = newQuantity;
                // Update price from ProductService — in case price changed
                existingItem.Price = product.Price;
                // Update stock from ProductService
                existingItem.Stock = product.Stock;

                // Save updated item to database
                var updated = await _cartRepo.UpdateAsync(existingItem);
                _logger.LogInformation("Cart item updated: UserId {UserId}, ProductId {ProductId}", userId, dto.ProductId);
                return MapToDto(updated);
            }

            // New product — create cart item with details from ProductService
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = dto.ProductId,
                // ProductName fetched from ProductService — not from user
                ProductName = product.Name,
                // Price fetched from ProductService — not from user
                Price = product.Price,
                Quantity = dto.Quantity,
                // Stock fetched from ProductService — used for validation
                Stock = product.Stock,
                // ImageUrl fetched from ProductService — not from user
                ImageUrl = product.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            // Save new item to database
            var created = await _cartRepo.AddAsync(cartItem);
            _logger.LogInformation("Cart item added: UserId {UserId}, ProductId {ProductId}", userId, dto.ProductId);
            return MapToDto(created);
        }

        // Update quantity of existing cart item
        public async Task<CartItemResponseDto> UpdateQuantityAsync(int userId, int cartItemId, UpdateCartDto dto)
        {
            // Validate inputs
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            if (cartItemId <= 0)
                throw new ArgumentException("Invalid cart item ID.");

            // Find cart item
            var cartItems = await _cartRepo.GetCartByUserIdAsync(userId);
            var cartItem = cartItems.FirstOrDefault(c => c.CartItemId == cartItemId);

            // Throw if cart item not found
            if (cartItem == null)
                throw new KeyNotFoundException($"Cart item with ID {cartItemId} was not found.");

            // Fetch latest stock from ProductService to validate quantity
            var product = await _productClient.GetProductAsync(cartItem.ProductId);

            // Stock validation — cannot exceed available stock
            if (dto.Quantity > product.Stock)
                throw new InvalidOperationException(
                    $"Only {product.Stock} items available in stock for '{cartItem.ProductName}'.");

            // Update quantity and latest stock
            cartItem.Quantity = dto.Quantity;
            cartItem.Stock = product.Stock;
            cartItem.Price = product.Price;

            // Save updated item to database
            var updated = await _cartRepo.UpdateAsync(cartItem);
            _logger.LogInformation("Cart item quantity updated: CartItemId {CartItemId}", cartItemId);
            return MapToDto(updated);
        }

        // Remove specific item from cart
        public async Task<bool> RemoveFromCartAsync(int userId, int cartItemId)
        {
            // Validate inputs
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            if (cartItemId <= 0)
                throw new ArgumentException("Invalid cart item ID.");

            // Remove cart item — returns false if not found
            var removed = await _cartRepo.RemoveAsync(cartItemId, userId);

            if (!removed)
                throw new KeyNotFoundException($"Cart item with ID {cartItemId} was not found.");

            _logger.LogInformation("Cart item removed: CartItemId {CartItemId}", cartItemId);
            return true;
        }

        // Clear all cart items for user — called after order is placed
        public async Task<bool> ClearCartAsync(int userId)
        {
            // Validate userId
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            // Clear cart — returns false if cart is already empty
            var cleared = await _cartRepo.ClearCartAsync(userId);

            if (!cleared)
                throw new InvalidOperationException("Cart is already empty.");

            _logger.LogInformation("Cart cleared for UserId {UserId}", userId);
            return true;
        }

        //  Map CartItem entity to CartItemResponseDto 

        private static CartItemResponseDto MapToDto(CartItem c) => new()
        {
            CartItemId = c.CartItemId,
            UserId = c.UserId,
            ProductId = c.ProductId,
            ProductName = c.ProductName,
            Price = c.Price,
            Quantity = c.Quantity,
            Stock = c.Stock,
            ImageUrl = c.ImageUrl,
            CreatedAt = c.CreatedAt
        };
    }
}
