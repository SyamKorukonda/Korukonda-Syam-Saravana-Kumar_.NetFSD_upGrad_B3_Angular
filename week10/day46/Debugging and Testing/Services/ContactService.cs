using WebApplication21.Models;

namespace WebApplication21.Services
{
    public class ContactService:IContactService
    {
        private readonly List<Contact> _contacts = new();

        public List<Contact> GetAll() => _contacts;

        public Contact GetById(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) throw new Exception("Contact not found");
            return contact;
        }

        public Contact Add(Contact contact)
        {
            contact.Id = _contacts.Count + 1;
            _contacts.Add(contact);
            return contact;
        }

        public void Update(int id, Contact contact)
        {
            var existing = GetById(id);
            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;
        }

        public void Delete(int id)
        {
            var contact = GetById(id);
            _contacts.Remove(contact);
        }
    }
}
