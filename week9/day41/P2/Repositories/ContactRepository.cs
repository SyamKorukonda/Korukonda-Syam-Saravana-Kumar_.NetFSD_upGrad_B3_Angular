using Microsoft.EntityFrameworkCore;
using WebApplication20.Models;

namespace WebApplication20.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Contact>, int)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Contacts.CountAsync();

            var data = await _context.Contacts
                .OrderBy(c => c.ContactId)
                .Skip((pageNumber - 1) * pageSize) // ⭐
                .Take(pageSize)                   // ⭐
                .ToListAsync();

            return (data, totalRecords);
        }
    }
}
