using ShopEZ_OrderService.DTOs;

namespace ShopEZ_OrderService.Services
{
    public interface IOrderService
    {
        // Interface defines what operations OrderService can perform
       
            Task<OrderResponseDto> CreateOrderAsync(int userId, OrderDto dto);
            Task<OrderResponseDto> CreateOrderFromCartAsync(int userId, string token);
            Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
            Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId);
            Task<OrderResponseDto?> GetOrderByIdAsync(int id);
            Task<bool> CancelOrderAsync(int orderId, int userId, bool isAdmin);
        
    }
}
