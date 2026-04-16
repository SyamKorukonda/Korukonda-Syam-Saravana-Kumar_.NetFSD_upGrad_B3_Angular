using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly JwtService _jwtService;

        public AuthenticateController(AuthDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto request)
        {
            
            var userObj = await _context.Users.FirstOrDefaultAsync(user =>
                user.Email == request.Email &&
                user.Password == request.Password);

            if (userObj != null)
            {
                string tokenStr = _jwtService.GenerateJSONWebToken(userObj);
                return Ok(new { token = tokenStr });
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists.");
            }

            var newUser = new UserModel
            {
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }
    }
}