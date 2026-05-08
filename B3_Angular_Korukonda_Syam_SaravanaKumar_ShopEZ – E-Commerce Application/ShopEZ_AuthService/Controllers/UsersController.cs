using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopEZ_AuthService.Common;
using ShopEZ_AuthService.DTOs;
using ShopEZ_AuthService.Services;

namespace ShopEZ_AuthService.Controllers
{
    // Defines the route for all endpoints -> api/users
    [Route("api/[controller]")]
    // Enables automatic model validation and API behaviors
    [ApiController]
    // Only Admin role can access this controller
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        // AuthService handles GetAllUsers business logic
        private readonly IAuthService _authService;

        // Constructor — Dependency Injection
        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET api/users — Returns all registered users (Admin only)
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                // Call AuthService to fetch all users from database
                var users = await _authService.GetAllUsersAsync();
                // Return 200 OK with list of users
                return Ok(new ApiResponse<IEnumerable<UserResponseDto>>(users, "Users fetched successfully"));
            }
            catch (Exception ex)
            {
                // ExceptionMiddleware handles logging — return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }
    }
}