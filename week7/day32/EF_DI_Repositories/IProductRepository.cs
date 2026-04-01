using WebApplication6.Models;
namespace WebApplication6.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);
        
        void Add(Product product);

        void Update(Product product);
        void Delete(int id);


    }
}
