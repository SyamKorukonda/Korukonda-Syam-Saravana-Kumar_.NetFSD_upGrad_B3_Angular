using ContactService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _context; 

        public ContactsController(ContactDbContext context)
        {
             _context = context;    
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact == null)
                return NotFound($"Contact with ID {id} not found.");
            return Ok(contact);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return Ok(new { contact, status = "New Contact successfully added to server..!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.ContactId)
                return BadRequest("Contact Id not matched");
            var oldContact = await _context.Contacts.FindAsync(id);
            if (oldContact == null)
                return NotFound($"contact id{id} not found");
            oldContact.Name = contact.Name;
            oldContact.Email = contact.Email;
            oldContact.Phone = contact.Phone;
            oldContact.CategoryId= contact.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(new { contact, status = "Contact updated sucessfully" });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound($"contact id{id} not found");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return Ok(new { contact, status = "Contact deleted sucessfully" });
        }
    }
}
