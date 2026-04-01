
using WebApplication6.Models;

namespace WebApplication6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.products.Find(id);
        }

        public void Add(Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        { 
        _context.products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var product= _context.products.Find(id);
            if(product != null)
            {
                _context.products.Remove(product);
                _context.SaveChanges();
            }
            
        }
                
    }
}
