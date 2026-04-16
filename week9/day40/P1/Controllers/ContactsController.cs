using ContactManagement.Dto;
using ContactManagement.Models;
using ContactManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require login
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repo;
        private readonly AppDbContext _context; // 👈 1. Add _context here

        public ContactsController(IContactRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _repo.GetAllAsync();

            var result = contacts.Select(c => new ContactDto
            {
                ContactId = c.ContactId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                EmailId = c.EmailId,
                MobileNo = c.MobileNo,
                Designation = c.Designation,
                CompanyId = c.CompanyId,
                DepartmentName = c.Department?.DepartmentName
            });

            return Ok(result); // 200 OK
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _repo.GetByIdAsync(id);

            if (contact == null)
                return NotFound(); // 404

            var result = new ContactDto
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                EmailId = contact.EmailId,
                MobileNo = contact.MobileNo,
                Designation = contact.Designation,
                CompanyId = contact.CompanyId,
                DepartmentName = contact.Department?.DepartmentName
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateContact(ContactDto dto)
        {
            var contact = new ContactInfo
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailId = dto.EmailId,
                MobileNo = dto.MobileNo,
                Designation = dto.Designation,
                CompanyId = dto.CompanyId,
                DepartmentId = dto.DepartmentId
            };

            await _repo.AddAsync(contact);

            return Ok(new { message = "Contact created successfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateContact(int id, ContactDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.EmailId = dto.EmailId;
            existing.MobileNo = dto.MobileNo;
            existing.Designation = dto.Designation;
            existing.CompanyId = dto.CompanyId;
            existing.DepartmentId = dto.DepartmentId;

            await _repo.UpdateAsync(existing);

            return Ok(new { message = "Contact updated successfully" });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _repo.GetByIdAsync(id);

            if (contact == null)
                return NotFound();

            await _repo.DeleteAsync(id);

            return Ok(new
            {
                message = "Contact deleted successfully"
            });
        }
    }
}