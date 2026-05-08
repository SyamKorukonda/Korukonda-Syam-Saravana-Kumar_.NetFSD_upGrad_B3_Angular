using ShopEZ_CartService.Models;

namespace ShopEZ_CartService.Repositories
{
    // Interface defines what operations are available on CartItems table

    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartByUserIdAsync(int userId);
        Task<CartItem?> GetCartItemAsync(int userId, int productId);
        Task<CartItem> AddAsync(CartItem cartItem);
        Task<CartItem> UpdateAsync(CartItem cartItem);
        Task<bool> RemoveAsync(int cartItemId, int userId);
        Task<bool> ClearCartAsync(int userId);
        Task<bool> ExistsAsync(int userId, int productId);
    }
}
