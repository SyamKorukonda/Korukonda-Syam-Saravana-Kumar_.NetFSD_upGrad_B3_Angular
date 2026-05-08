using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopEZ_AuthService.Common;
using ShopEZ_AuthService.DTOs;
using ShopEZ_AuthService.Services;

namespace ShopEZ_AuthService.Controllers
{
    // Defines the route for all endpoints -> api/auth
    [Route("api/[controller]")]
    // Enables automatic model validation and API behaviors
    [ApiController]
    public class AuthController : ControllerBase
    {
        // AuthService handles Register and Login business logic
        private readonly IAuthService _authService;

        // Constructor — Dependency Injection
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                // Check if the incoming request data is valid based on DTO annotations
                if (!ModelState.IsValid)
                {
                    // Collect all validation error messages and join them
                    var errors = string.Join(", ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return BadRequest(new ApiResponse<string>($"Validation failed: {errors}"));
                }
                // Call AuthService to register the user
                var result = await _authService.RegisterAsync(dto);
                // Return 200 OK with success message
                return Ok(new ApiResponse<string>(result, "Registration successful"));
            }
            catch (Exception ex)
            {
                // ExceptionMiddleware handles logging — return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                // Check if the incoming request data is valid based on DTO annotations
                if (!ModelState.IsValid)
                {
                    // Collect all validation error messages and join them
                    var errors = string.Join(", ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return BadRequest(new ApiResponse<string>($"Validation failed: {errors}"));
                }
                // Call AuthService to validate credentials and generate JWT token
                var result = await _authService.LoginAsync(dto);
                // Return 200 OK with JWT token, role and message
                return Ok(new ApiResponse<AuthResponseDto>(result, "Login successful"));
            }
            catch (Exception ex)
            {
                // ExceptionMiddleware handles logging — return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }
    }
}