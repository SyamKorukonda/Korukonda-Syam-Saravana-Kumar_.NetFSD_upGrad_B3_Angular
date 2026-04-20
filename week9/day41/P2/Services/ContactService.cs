using Microsoft.Extensions.Caching.Memory;
using WebApplication20.Repositories;
using WebApplication20.Models;

namespace WebApplication20.Services
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _repo;

        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<Contact>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (data, totalRecords) = await _repo.GetPagedAsync(pageNumber, pageSize);

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return new PagedResponse<Contact>
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Data = data
            };
        }
    }
}
