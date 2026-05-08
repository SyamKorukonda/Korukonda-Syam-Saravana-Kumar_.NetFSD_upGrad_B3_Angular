using Microsoft.EntityFrameworkCore;
using WebApplication17.DTOs;
using WebApplication17.Models;

namespace WebApplication17.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // Check if the email already exists in the database
            // Updated to use EmailAddress
            if (await _context.Users.AnyAsync(u => u.EmailAddress == dto.Email))
            {
                throw new ArgumentException("Email is already registered.");
            }

            //  Map the DTO to the User entity
            var user = new User
            {
                UserName = dto.Name,
                EmailAddress = dto.Email,

                //BCrypt is used for password hashing because it provides salting and is resistant to brute-force attacks.
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                //Default role = Customer if not provided
                Role = string.IsNullOrEmpty(dto.Role) ? "Customer" : dto.Role
            };

            // Save to database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully.";
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            // Find the user by their email
            // Updated to use EmailAddress
            var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailAddress == dto.Email);

            if (user == null)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            //  Verify the provided password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new ArgumentException("Invalid email or password.");
            }

            //  Generate and return the JWT token

            string jwtToken= _jwtService.GenerateJSONWebToken(user);
            return jwtToken;
        }
    }
}