using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.ApiResponse;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Only Admins can get the users data 
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // Endpoint: GET api/Users
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.Select(u => new
            {
                // Fetch users from database
                // Selecting only required fields (security: avoid exposing PasswordHash)

                u.UserId,
                u.UserName,
                u.EmailAddress,
                u.Role
            }).ToListAsync();

            // Return users list in standardized response format
            return Ok(new ApiResponse<object>(users, "Users fetched successfully"));

        }
    }
}
