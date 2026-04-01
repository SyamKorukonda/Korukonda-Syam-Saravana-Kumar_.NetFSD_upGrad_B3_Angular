using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) // constructor injection
        {
            _contactService = contactService;
        }

        // show contacts
        public IActionResult Index()
        {
            var data = _contactService.GetAllContacts();
            return View(data);
        }

        //Get contact by Id
        public IActionResult GetContact(int id)
        {
            var contact=_contactService.GetContactById(id); 
            return View(contact);
        }

        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(ContactInfo contactInfo)
        {
            if(ModelState.IsValid)
            {
                _contactService.AddContact(contactInfo);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
