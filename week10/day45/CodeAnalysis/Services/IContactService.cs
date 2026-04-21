using WebApplication21.Models;
namespace WebApplication21.Services
{
    public interface IContactService
    {
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int id);
        IReadOnlyList<Contact> GetAllContacts();
    }
}
