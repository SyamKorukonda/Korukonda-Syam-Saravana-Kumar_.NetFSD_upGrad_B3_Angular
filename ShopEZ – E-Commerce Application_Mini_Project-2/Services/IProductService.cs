using WebApplication17.DTOs;

namespace WebApplication17.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateUpdateDto dto);
        Task UpdateAsync(int id, ProductCreateUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
