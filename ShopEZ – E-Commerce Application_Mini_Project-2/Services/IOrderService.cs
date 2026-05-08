using WebApplication17.DTOs;

namespace WebApplication17.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(int userId, OrderDto dto);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId);
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
    }
}
