using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models;
using WebApplication10.Repositories;


namespace WebApplication10.Controllers
{
    public class ContactsController : Controller
    {
        

        private readonly IContactRepository _repo;

        public ContactsController(IContactRepository repo)
        {
            _repo = repo;
        }

        // Index
        public IActionResult Index()
        {
            return View(_repo.GetAllContacts());
        }

        //  Details
        public IActionResult Details(int id)
        {
            var contact = _repo.GetContactById(id);
            return View(contact);
        }

        //  Create (GET)
        public IActionResult Create()
        {
            ViewBag.Companies = _repo.GetCompanies();
            ViewBag.Departments = _repo.GetDepartments();
            return View();
        }

        //  Create (POST)
        [HttpPost]
        public IActionResult Create(ContactInfo contact)
        {
            _repo.AddContact(contact);
            return RedirectToAction("Index");
        }

        //  Edit (GET)
        public IActionResult Edit(int id)
        {
            var contact = _repo.GetContactById(id);

            ViewBag.Companies = _repo.GetCompanies();
            ViewBag.Departments = _repo.GetDepartments();

            return View(contact);
        }

        // Edit (POST)
        [HttpPost]
        public IActionResult Edit(ContactInfo contact)
        {
            
                _repo.UpdateContact(contact);
                return RedirectToAction("Index");
           
           
        }

        //  Delete (GET)
        public IActionResult Delete(int id)
        {
            var contact = _repo.GetContactById(id);
            return View(contact);
        }

        //  Delete (POST)
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _repo.DeleteContact(id);
            return RedirectToAction("Index");
        }
        


    }
}
