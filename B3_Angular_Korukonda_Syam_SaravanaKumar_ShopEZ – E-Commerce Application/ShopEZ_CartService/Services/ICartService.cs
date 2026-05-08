using ShopEZ_CartService.DTOs;

namespace ShopEZ_CartService.Services
{
    // Interface defines what operations CartService can perform

    public interface ICartService
    {
        Task<CartSummaryDto> GetCartAsync(int userId);
        Task<CartItemResponseDto> AddToCartAsync(int userId, AddToCartDto dto);
        Task<CartItemResponseDto> UpdateQuantityAsync(int userId, int cartItemId, UpdateCartDto dto);
        Task<bool> RemoveFromCartAsync(int userId, int cartItemId);
        Task<bool> ClearCartAsync(int userId);
    }
}
