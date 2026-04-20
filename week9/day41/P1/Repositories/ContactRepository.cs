using WebApplication20.Models;

namespace WebApplication20.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private static List<Contact> _contacts = new()
        {
            new Contact { Id = 1, Name = "John", Email = "john@gmail.com" },
            new Contact { Id = 2, Name = "Alice", Email = "alice@gmail.com" }
        };

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            await Task.Delay(500); // simulate DB delay
            return _contacts;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            await Task.Delay(500); // simulate DB delay
            return _contacts.FirstOrDefault(x => x.Id == id);
        }
    }
}
