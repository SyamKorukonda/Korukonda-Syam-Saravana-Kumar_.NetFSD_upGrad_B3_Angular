using Microsoft.EntityFrameworkCore;
using ShopEZ_ProductService.Models;

namespace ShopEZ_ProductService.Data
{
    public class ProductDbContext:DbContext
    {
        // Constructor — passes options to base DbContext
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        // Products table
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Index on Name for faster search queries
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);
        }
    }
}
