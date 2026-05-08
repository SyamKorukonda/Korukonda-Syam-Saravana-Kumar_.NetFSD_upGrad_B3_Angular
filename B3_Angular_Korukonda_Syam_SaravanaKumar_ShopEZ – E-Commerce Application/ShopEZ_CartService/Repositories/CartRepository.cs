using ShopEZ_CartService.Data;
using ShopEZ_CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopEZ_CartService.Repositories
{
    public class CartRepository:ICartRepository
    {
        // EF Core DbContext to interact with CartItems table
        private readonly CartDbContext _context;

        // Constructor — Dependency Injection
        public CartRepository(CartDbContext context)
        {
            _context = context;
        }

        // Get all cart items for a specific user
        public async Task<IEnumerable<CartItem>> GetCartByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .AsNoTracking()
                // Filter cart items for specific user
                .Where(c => c.UserId == userId)
                // Latest items first
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // Get specific cart item by userId and productId
        public async Task<CartItem?> GetCartItemAsync(int userId, int productId)
        {
            return await _context.CartItems
                // Find cart item by userId and productId
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }

        // Add new item to cart
        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            // Add cart item to EF Core change tracker
            _context.CartItems.Add(cartItem);
            // Save changes to database
            await _context.SaveChangesAsync();
            return cartItem;
        }

        // Update existing cart item
        public async Task<CartItem> UpdateAsync(CartItem cartItem)
        {
            // Update cart item in EF Core change tracker
            _context.CartItems.Update(cartItem);
            // Save changes to database
            await _context.SaveChangesAsync();
            return cartItem;
        }

        // Remove specific cart item by cartItemId and userId
        public async Task<bool> RemoveAsync(int cartItemId, int userId)
        {
            // Find cart item by ID and userId — ensures user can only remove their own items
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.CartItemId == cartItemId && c.UserId == userId);

            // Return false if cart item not found
            if (cartItem == null) return false;

            // Remove cart item from EF Core change tracker
            _context.CartItems.Remove(cartItem);
            // Save changes to database
            await _context.SaveChangesAsync();
            return true;
        }

        // Clear all cart items for a specific user — called after order is placed
        public async Task<bool> ClearCartAsync(int userId)
        {
            // Get all cart items for user
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            // Return false if cart is already empty
            if (!cartItems.Any()) return false;

            // Remove all cart items for user
            _context.CartItems.RemoveRange(cartItems);
            // Save changes to database
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if product already exists in user cart
        public async Task<bool> ExistsAsync(int userId, int productId)
        {
            return await _context.CartItems
                .AnyAsync(c => c.UserId == userId && c.ProductId == productId);
        }
    }
}
