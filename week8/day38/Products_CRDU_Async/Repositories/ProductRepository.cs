using WebApplication12.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication12.Repositories
{
    public class ProductRepository: IProductRepository
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

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        //public async Task<Product> UpdateAsync(Product product)
        // {
        //     var p1=await _context.Products.FirstOrDefaultAsync(x=>x.Id==product.Id);
        //     if (p1 == null)
        //         return null;
        //     _context.Products.Update(product);
        //     await _context.SaveChangesAsync();
        //     return product;
        // }

        public async Task<Product> UpdateAsync(Product product)
        {
            var p1 = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (p1 == null)
                return null;

            //  Update existing tracked entity
            p1.Name = product.Name;
            p1.Price = product.Price;
            p1.Category = product.Category;

            await _context.SaveChangesAsync();

            return p1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product=await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
