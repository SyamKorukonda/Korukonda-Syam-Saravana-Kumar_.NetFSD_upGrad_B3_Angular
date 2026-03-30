using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ContactController : Controller
    {
        public static List<ContactInfo> contacts= new List<ContactInfo>();

        //show all Contacts
        public IActionResult ShowContacts()
        {
            return View(contacts);
        }

        //search by ID
        public IActionResult GetContactById(int id)
        {
            var contact=contacts.FirstOrDefault(c =>c.ContactId==id);
            return View(contact);

        /*  var searchResult = contacts.Select(c => c);
            if(id!=0)
            {
                searchResult=searchResult.Where(c=>c.ContactId==id);
            }
            return View(searchResult.ToList());  */
        }

        // Add contact
        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(ContactInfo contactInfo)
        {
            contacts.Add(contactInfo);
            return RedirectToAction("ShowContacts");
        }
    }
}
