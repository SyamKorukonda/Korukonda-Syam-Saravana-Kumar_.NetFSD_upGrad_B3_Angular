using ShopEZ_ProductService.DTOs;

namespace ShopEZ_ProductService.Services
{
    // Interface defines what operations ProductService can perform
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateUpdateDto dto);
        Task<ProductDto> UpdateAsync(int id, ProductCreateUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
    }
}
