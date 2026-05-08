using Microsoft.EntityFrameworkCore;
using ShopEZ_CartService.Models;

namespace ShopEZ_CartService.Data
{
    public class CartDbContext:DbContext
    {
        // Constructor — passes options to base DbContext
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

        // CartItems table
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Index on UserId for faster cart lookups per user
            modelBuilder.Entity<CartItem>()
                .HasIndex(c => c.UserId);

            // Unique constraint — one user cannot have same product twice in cart
            // Instead quantity is updated
            modelBuilder.Entity<CartItem>()
                .HasIndex(c => new { c.UserId, c.ProductId })
                .IsUnique();
        }
    }
}
