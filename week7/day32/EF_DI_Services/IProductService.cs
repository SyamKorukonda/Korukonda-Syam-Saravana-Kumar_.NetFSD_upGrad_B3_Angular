using WebApplication6.Models;
namespace WebApplication6.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();

        Product GetProduct(int id);

        void CreateProduct(Product product);
        void UpdateProduct(Product product);    
        void DeleteProduct(int id);


    }
}
