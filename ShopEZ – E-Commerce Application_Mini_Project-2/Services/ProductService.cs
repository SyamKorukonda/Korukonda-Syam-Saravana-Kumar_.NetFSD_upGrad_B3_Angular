using WebApplication17.DTOs;
using WebApplication17.Models;
using WebApplication17.Repositories;

namespace WebApplication17.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            // Fetch all products from repository
            var products = await _repo.GetAllAsync();
            // Map Product entity to ProductDto
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) throw new ArgumentException("Product not found");

            // Map entity to DTO
            return new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock
            };
        }

        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto dto)
        {
            // Map DTO to Product entity
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Stock = dto.Stock
            };

            var created = await _repo.AddAsync(product);

            // Return created product as DTO
            return new ProductDto
            {
                ProductId = created.ProductId,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                ImageUrl = created.ImageUrl,
                Stock = created.Stock
            };
        }

        public async Task UpdateAsync(int id, ProductCreateUpdateDto dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) throw new ArgumentException("Product not found");

            // Update fields
            p.Name = dto.Name;
            p.Description = dto.Description;
            p.Price = dto.Price;
            p.ImageUrl = dto.ImageUrl;
            p.Stock = dto.Stock;

            await _repo.UpdateAsync(p);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
