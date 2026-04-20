using Microsoft.Extensions.Caching.Memory;
using WebApplication20.Repositories;
using WebApplication20.Models;

namespace WebApplication20.Services
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _repo;
        private readonly IMemoryCache _cache;

        public ContactService(IContactRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            string cacheKey = "contacts";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Contact> contacts))
            {
                contacts = await _repo.GetAllAsync();

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                _cache.Set(cacheKey, contacts, options);
            }

            return contacts;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            string cacheKey = $"contact_{id}";

            if (!_cache.TryGetValue(cacheKey, out Contact contact))
            {
                contact = await _repo.GetByIdAsync(id);

                if (contact != null)
                {
                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                    _cache.Set(cacheKey, contact, options);
                }
            }

            return contact;
        }
    }
}
