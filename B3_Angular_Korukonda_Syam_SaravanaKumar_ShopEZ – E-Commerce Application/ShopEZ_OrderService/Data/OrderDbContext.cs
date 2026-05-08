using Microsoft.EntityFrameworkCore;
using ShopEZ_OrderService.Models;

namespace ShopEZ_OrderService.Data
{
    public class OrderDbContext:DbContext
    {
        // Constructor — passes options to base DbContext
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        // Orders table
        public DbSet<Order> Orders { get; set; }

        // OrderItems table
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One Order has many OrderItems
            // Cascade delete — deleting order also deletes its items
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index on UserId for faster user order lookups
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserId);
        }
    }
}
