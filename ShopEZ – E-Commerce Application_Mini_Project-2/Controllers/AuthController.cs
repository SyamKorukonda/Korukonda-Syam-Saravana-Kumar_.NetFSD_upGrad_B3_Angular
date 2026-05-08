using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.ApiResponse;
using WebApplication17.DTOs;
using WebApplication17.Services;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")] //  // Defines API route: api/Auth
    [ApiController] // Enables automatic model validation & API behaviors
    public class AuthController : ControllerBase
    {
        // Dependency Injection of AuthService
        private readonly IAuthService _authService;

        // Constructor to inject service
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        // Endpoint: POST api/Auth/register
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Call service to register user
            var result = await _authService.RegisterAsync(dto);
            return Ok(new ApiResponse<string>(result, "Registration successful"));
        }

        [HttpPost("login")]
        // Endpoint: POST api/Auth/login
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Call service to authenticate user and generate JWT token
            var token = await _authService.LoginAsync(dto);
            return Ok(new ApiResponse<string>(token, "Login successful"));
        }
    }
}
