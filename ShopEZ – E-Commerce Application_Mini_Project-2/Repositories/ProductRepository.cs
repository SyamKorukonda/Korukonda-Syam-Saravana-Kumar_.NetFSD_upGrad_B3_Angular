using Microsoft.EntityFrameworkCore;
using WebApplication17.Models;

namespace WebApplication17.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            // Find product by primary key
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateAsync(Product product)
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
        
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(item => item.ProductId == id);
            if (product == null) { return false; }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
