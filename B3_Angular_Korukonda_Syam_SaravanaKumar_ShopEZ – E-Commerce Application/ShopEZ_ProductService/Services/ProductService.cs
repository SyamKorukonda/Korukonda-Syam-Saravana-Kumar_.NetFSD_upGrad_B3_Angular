using ShopEZ_ProductService.DTOs;
using ShopEZ_ProductService.Models;
using ShopEZ_ProductService.Repositories;

namespace ShopEZ_ProductService.Services
{
    public class ProductService:IProductService
    {
        // ProductRepository to interact with Products table using EF Core
        private readonly IProductRepository _repo;
        private readonly ILogger<ProductService> _logger;

        // Constructor — Dependency Injection
        public ProductService(IProductRepository repo, ILogger<ProductService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // Get all products
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            // Fetch all products from repository
            var products = await _repo.GetAllAsync();
            // Map each Product entity to ProductDto
            return products.Select(MapToDto);
        }

        // Get single product by ID
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            // Validate ID
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than 0.");

            // Fetch product from repository
            var product = await _repo.GetByIdAsync(id);
            // Throw if not found — caught by controller catch block
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} was not found.");

            return MapToDto(product);
        }

        // Get single product by Category
        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
        {
            var products = await _repo.GetAllAsync();

            return products
                .Where(p => p.Category.ToLower() == category.ToLower())
                .OrderByDescending(p => p.ProductId)
                .Select(MapToDto);
        }

        // Create new product
        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto dto)
        {
            // Map DTO to Product entity
            var product = new Product
            {
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim(),
                Price = dto.Price,
                ImageUrl = dto.ImageUrl?.Trim(),
                Stock = dto.Stock,
                Category = dto.Category.Trim()
            };

            // Save to database using repository
            var created = await _repo.AddAsync(product);
            _logger.LogInformation("Product created: {ProductId} - {Name}", created.ProductId, created.Name);
            return MapToDto(created);
        }

        // Update existing product
        public async Task<ProductDto> UpdateAsync(int id, ProductCreateUpdateDto dto)
        {
            // Validate ID
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than 0.");

            // Check if product exists
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} was not found.");

            // Update fields
            product.Name = dto.Name.Trim();
            product.Description = dto.Description.Trim();
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl?.Trim();
            product.Stock = dto.Stock;
            product.Category = dto.Category.Trim();

            // Save updated product to database
            await _repo.UpdateAsync(product);
            _logger.LogInformation("Product updated: {ProductId}", id);
            return MapToDto(product);
        }

        // Delete product by ID
        public async Task DeleteAsync(int id)
        {
            // Validate ID
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than 0.");

            // Delete product from database — returns false if not found
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                throw new KeyNotFoundException($"Product with ID {id} was not found.");

            _logger.LogInformation("Product deleted: {ProductId}", id);
        }

        // Map Product entity to ProductDto 

        private static ProductDto MapToDto(Product p) => new()
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Stock = p.Stock,
            Category = p.Category
        };
    }
}
