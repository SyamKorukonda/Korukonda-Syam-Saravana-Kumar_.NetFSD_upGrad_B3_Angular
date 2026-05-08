using Microsoft.EntityFrameworkCore;
using WebApplication17.Models;

namespace WebApplication17.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
         public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Saves the Order and associated OrderItems automatically
            return order;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)  // Include related OrderItems
                .ThenInclude(oi => oi.Product) // Include Product details inside each OrderItem
                .ToListAsync(); // Execute query and return list
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId) // Filter orders for specific user
                .ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);  // Find order by primary key
        }

    }
}
