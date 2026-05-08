using ShopEZ_OrderService.Models;

namespace ShopEZ_OrderService.Repositories
{
    // Interface defines what operations are available on Orders table
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> CancelOrderAsync(int orderId);
    }
}
