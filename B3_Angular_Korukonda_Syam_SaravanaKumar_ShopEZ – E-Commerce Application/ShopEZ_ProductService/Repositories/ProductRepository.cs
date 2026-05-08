using Microsoft.EntityFrameworkCore;
using ShopEZ_ProductService.Data;
using ShopEZ_ProductService.Models;

namespace ShopEZ_ProductService.Repositories
{
    public class ProductRepository:IProductRepository
    {
        // EF Core DbContext to interact with Products table
        private readonly ProductDbContext _context;

        // Constructor — Dependency Injection
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        // Get all products 
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.OrderByDescending(p => p.ProductId).ToListAsync();
        }

        // Get single product by ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            // FindAsync — searches by primary key, returns null if not found
            return await _context.Products.FindAsync(id);
        }

        // Add new product to database
        public async Task<Product> AddAsync(Product product)
        {
            // Add product to EF Core change tracker
            _context.Products.Add(product);
            // Save changes to database
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            // Check if it exists, without tracking it
            var existingProduct = await _context.Products
                .AsNoTracking() // Avoid tracking conflicts
                .FirstOrDefaultAsync(item => item.ProductId == product.ProductId); // Using ProductId

            if (existingProduct == null) { return null; }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Delete product by ID
        public async Task<bool> DeleteAsync(int id)
        {
            // Find product by ID
            var product = await _context.Products.FindAsync(id);
            // Return false if product not found
            if (product == null) return false;

            // Remove product from EF Core change tracker
            _context.Products.Remove(product);
            // Save changes to database
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
