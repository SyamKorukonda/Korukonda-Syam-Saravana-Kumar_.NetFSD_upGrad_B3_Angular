using WebApplication10.Models;

namespace WebApplication10.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<ContactInfo> GetAllContacts();
        ContactInfo GetContactById(int id);
        void AddContact(ContactInfo contact);
        void UpdateContact(ContactInfo contact);
        void DeleteContact(int id);

        // Dropdowns
        IEnumerable<Company> GetCompanies();
        IEnumerable<Department> GetDepartments();
    }
}
