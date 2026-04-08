using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private static List<Contact> contacts = new List<Contact>()
        {
            new Contact{ContactId = 1, FirstName = "karthik", LastName = "Kumar", EmailId = "karthik@gmail.com", MobileNo = 9876543210, Designation = "Developer", CompanyId = 1, DepartmentId = 1 },
            new Contact{ContactId = 2, FirstName = "Abbavaram", LastName = "Kumar", EmailId = "abbaavaram@gmail.com", MobileNo = 9876543211, Designation = "Developer", CompanyId = 2, DepartmentId = 2 },
            new Contact{ContactId = 3, FirstName = "kiran", LastName = "Kumar", EmailId = "Kiran@gmail.com", MobileNo = 9876543212, Designation = "Developer", CompanyId = 3, DepartmentId = 3 }
        };

        private static List<Company> companies = new List<Company>()
        {
            new Company{CompanyId=1,CompanyName="CTS"},
            new Company{CompanyId=2,CompanyName="TCS"},
            new Company{CompanyId=3,CompanyName="ITC"},
        };

        private static List<Department> departments = new List<Department>()
        {
            new Department{DepartmentId=1,DepartmentName="IT" },
            new Department{DepartmentId=2,DepartmentName="HR" },
            new Department{DepartmentId=3,DepartmentName="Marketing" }
        };

        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = contacts.Find(x => x.ContactId == id);
            if (contact == null)
            {
                return NotFound("requested contactid is not there  ");
            }
            else
            {
                return Ok(contact);
            }
        }

        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            contacts.Add(contact);
            return Ok(new { contact, status = "New contact add sucessfully" });
        }

        [HttpPut("{id}")]
        public IActionResult EditContact( int id,Contact contact)
        {
            var oldContact=contacts.Find(x => x.ContactId == id);
            if (oldContact == null)
            {
                return NotFound("Contact id to update is  not found");
            }
            else
            {
                oldContact.FirstName = contact.FirstName;
                oldContact.LastName = contact.LastName;
                oldContact.EmailId = contact.EmailId;
                oldContact.MobileNo = contact.MobileNo; 
                oldContact.Designation = contact.Designation;
                oldContact.CompanyId = contact.CompanyId;
                oldContact.DepartmentId = contact.DepartmentId;

                return Ok(new {UpdatedContact = oldContact,status="Contact is updated Sucessfully"});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = contacts.Find(x => x.ContactId == id);
            if (contact == null)
            {
                return NotFound("Contact id to update is  not found");
            }
            else
            {
                contacts.Remove(contact);
                return Ok(new { contact, status = "Contact is deleted sucessfully" });
            }
        }



    }
}
