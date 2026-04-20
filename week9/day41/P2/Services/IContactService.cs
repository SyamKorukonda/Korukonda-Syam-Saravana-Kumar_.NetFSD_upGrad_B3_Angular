using WebApplication20.Models;

namespace WebApplication20.Services
{
    public interface IContactService
    {
        Task<PagedResponse<Contact>> GetPagedAsync(int pageNumber, int pageSize);

    }
}
