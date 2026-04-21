using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication21.Models;
using WebApplication21.Services;

namespace WebApplication21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService service, ILogger<ContactsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/contacts
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Fetching all contacts");

            var contacts = _service.GetAll();
            return Ok(contacts);
        }

        // GET: api/contacts/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var contact = _service.GetById(id);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching contact");
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/contacts
        [HttpPost]
        public IActionResult Add([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest("Contact data is required");

            if (string.IsNullOrWhiteSpace(contact.Name))
                return BadRequest("Name is required");

            var created = _service.Add(contact);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/contacts/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Contact contact)
        {
            try
            {
                _service.Update(id, contact);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating contact");
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/contacts/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting contact");
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
