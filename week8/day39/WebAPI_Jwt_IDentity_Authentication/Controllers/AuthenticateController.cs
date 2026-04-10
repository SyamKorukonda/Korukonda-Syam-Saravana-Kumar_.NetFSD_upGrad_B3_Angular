using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication16.Models;
using WebApplication16.Services;

namespace WebApplication16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticateController(JwtService jwtService, UserManager<ApplicationUser> userManager)
        {

            _jwtService = jwtService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            // 1. Verify the user credentials
            ApplicationUser? user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            // 2. Generate JWT Token
            string tokenStr = await _jwtService.GenerateJSONWebToken(user);
            return Ok(new { token = tokenStr });

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            var user = new ApplicationUser { UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Assign role
            if (!await _userManager.IsInRoleAsync(user, createUserDto.Role))
            {
                await _userManager.AddToRoleAsync(user, createUserDto.Role);
            }

            return Ok("User created");
        }
    }
}
