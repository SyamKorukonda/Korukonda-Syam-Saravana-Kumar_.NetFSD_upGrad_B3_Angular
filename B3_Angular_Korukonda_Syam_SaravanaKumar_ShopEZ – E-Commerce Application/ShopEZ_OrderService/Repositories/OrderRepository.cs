using ShopEZ_OrderService.Data;
using ShopEZ_OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopEZ_OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        // EF Core DbContext to interact with Orders table
        private readonly OrderDbContext _context;

        // Constructor — Dependency Injection
        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        // Create new order — saves order and order items together
        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Add order to EF Core change tracker
            _context.Orders.Add(order);
            // Save changes — saves Order and OrderItems automatically
            await _context.SaveChangesAsync();
            return order;
        }

        // Get all orders — used by Admin
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                // Include related OrderItems for each order
                .Include(o => o.OrderItems)
                // Latest orders first
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // Get orders by UserId — used by Customer to see their own orders
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                // Filter orders for specific user
                .Where(o => o.UserId == userId)
                // Latest orders first
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // Get single order by ID
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                // Find order by primary key
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        // Cancel order — soft delete by setting IsCancelled = true
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            // Find the order by ID
            var order = await _context.Orders.FindAsync(orderId);

            // Return false if order not found
            if (order == null) return false;

            // Return false if order is already cancelled
            if (order.IsCancelled) return false;

            // Mark order as cancelled — soft delete
            order.IsCancelled = true;

            // Save changes to database
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
