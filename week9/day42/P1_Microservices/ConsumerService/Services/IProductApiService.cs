using Consumer_Service.Models;

namespace Consumer_Service.Services
{
    public interface IProductApiService
    {
        Task<Product> GetProductId(int id);
        Task<List<Product>> GetProducts();
        Task<Product> AddProduct(Product product);

        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);
    }
}
