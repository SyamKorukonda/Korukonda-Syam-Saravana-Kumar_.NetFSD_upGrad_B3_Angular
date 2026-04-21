using WebApplication21.Models;
using WebApplication21.Services;

namespace WebApplication21
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IContactService service = new ContactService();

            // Add contacts
            service.AddContact(new Contact
            {
                Name = "Syam",
                Email = "syam@gmail.com",
                Phone = "9876543210"
            });

            service.AddContact(new Contact
            {
                Name = "John",
                Email = "john@gmail.com",
                Phone = "1234567890"
            });

            // Display contacts
            var contacts = service.GetAllContacts();

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Id} - {contact.Name} - {contact.Email}");
            }
        }
    }
}