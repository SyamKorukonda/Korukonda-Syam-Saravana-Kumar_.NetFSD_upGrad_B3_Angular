using WebApplication21.Models;

namespace WebApplication21.Services
{
    public class ContactService:IContactService
    {
        private readonly List<Contact> _contacts = new();

        public void AddContact(Contact contact)
        {
            ValidateContact(contact);

            contact.Id = GenerateId();
            _contacts.Add(contact);
        }

        public void UpdateContact(Contact contact)
        {
            ValidateContact(contact);

            var existing = FindContactById(contact.Id);

            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;
        }

        public void DeleteContact(int id)
        {
            var contact = FindContactById(id);
            _contacts.Remove(contact);
        }

        public IReadOnlyList<Contact> GetAllContacts()
        {
            return _contacts.AsReadOnly();
        }

        // Helper Methods (reduces duplication & complexity)

        private Contact FindContactById(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);

            if (contact is null)
                throw new ArgumentException($"Contact with Id {id} not found");

            return contact;
        }

        private static void ValidateContact(Contact contact)
        {
            if (contact is null)
                throw new ArgumentNullException(nameof(contact));

            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new ArgumentException("Email is required");
        }

        private int GenerateId()
        {
            return _contacts.Count == 0 ? 1 : _contacts.Max(c => c.Id) + 1;
        }
    }
}
