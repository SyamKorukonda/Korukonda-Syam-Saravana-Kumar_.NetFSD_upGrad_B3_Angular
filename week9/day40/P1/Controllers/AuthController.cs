// Controllers/AuthController.cs
using ContactManagement.Dto;
using ContactManagement.Models;
using ContactManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    // Inject AppDbContext and IJwtService
    public AuthController(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto request)
    {
        // Note: In a real app, hash the password before saving!
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Role = request.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

        if (user == null)
            return BadRequest("Invalid credentials");

        // Use the service to generate the token
        var token = _jwtService.GenerateJwtToken(user);

        return Ok(new { token });
    }
}