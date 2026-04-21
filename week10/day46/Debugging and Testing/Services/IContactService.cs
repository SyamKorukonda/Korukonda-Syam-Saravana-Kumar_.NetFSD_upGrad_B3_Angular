using WebApplication21.Models;
namespace WebApplication21.Services
{
    public interface IContactService
    {
        List<Contact> GetAll();
        Contact GetById(int id);
        Contact Add(Contact contact);
        void Update(int id, Contact contact);
        void Delete(int id);
    }
}
