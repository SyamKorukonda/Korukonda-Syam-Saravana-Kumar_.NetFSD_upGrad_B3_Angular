using ShopEZ_AuthService.DTOs;
using ShopEZ_AuthService.Models;
using ShopEZ_AuthService.Repositories;

namespace ShopEZ_AuthService.Services
{
    // Interface defines what operations AuthService can perform
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    }
    public class AuthService:IAuthService
    {
        // UserRepository to interact with Users table using Dapper
        private readonly IUserRepository _userRepo;
        // JwtService to generate JWT token after successful login
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        // Constructor — Dependency Injection
        public AuthService(
            IUserRepository userRepo,
            IJwtService jwtService,
            ILogger<AuthService> logger)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _logger = logger;
        }

        //  User Register

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // Validate inputs beyond data annotations
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name cannot be empty or whitespace.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email cannot be empty or whitespace.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password cannot be empty or whitespace.");

            // Check duplicate email using UserRepository
            bool emailExists = await _userRepo.EmailExistsAsync(dto.Email);
            if (emailExists)
                throw new ArgumentException("An account with this email already exists.");
            
            // if email not exists  then register the user
            var user = new User
            {
                UserName = dto.Name.Trim(),
                EmailAddress = dto.Email.Trim().ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "Customer" : dto.Role
            };

            // here we register or inserted data in to the database using UserRepository
            await _userRepo.CreateAsync(user);

            // add  the information to the logger
            _logger.LogInformation("User registered: {Email} with Role: {Role}", user.EmailAddress, user.Role);
            return "User registered successfully.";
        }

        // User Login

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password cannot be empty.");

            var user = await _userRepo.GetByEmailAsync(dto.Email);

            // Same message for both invalid email and wrong password
            //  prevents email enumeration attacks
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new ArgumentException("Invalid email or password.");
            
            // generate the user token
            var token = _jwtService.GenerateToken(user);

            // add the info to the logger
            _logger.LogInformation("User logged in: {Email}", user.EmailAddress);

            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role,
                Message = "Login successful"
            };
        }

        // Get all Users in the database

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                EmailAddress = u.EmailAddress,
                Role = u.Role
            });
        }
    }
}
