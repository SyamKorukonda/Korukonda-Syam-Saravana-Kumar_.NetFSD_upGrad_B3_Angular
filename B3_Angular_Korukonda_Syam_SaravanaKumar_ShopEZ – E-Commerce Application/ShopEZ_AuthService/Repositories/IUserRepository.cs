using ShopEZ_AuthService.Models;

namespace ShopEZ_AuthService.Repositories
{
    public interface IUserRepository
    {

        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> EmailExistsAsync(string email);
        Task<int> CreateAsync(User user);
    }
}
