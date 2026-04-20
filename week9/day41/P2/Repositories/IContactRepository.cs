using WebApplication20.Models;

namespace WebApplication20.Repositories
{
    public interface IContactRepository
    {
        Task<(IEnumerable<Contact>, int)> GetPagedAsync(int pageNumber, int pageSize);

    }

}
