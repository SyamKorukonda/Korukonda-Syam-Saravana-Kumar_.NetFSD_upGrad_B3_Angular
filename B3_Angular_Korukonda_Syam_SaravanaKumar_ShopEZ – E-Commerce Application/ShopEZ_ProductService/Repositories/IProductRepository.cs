using ShopEZ_ProductService.Models;

namespace ShopEZ_ProductService.Repositories
{
    // Interface defines what operations are available on Products table
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
