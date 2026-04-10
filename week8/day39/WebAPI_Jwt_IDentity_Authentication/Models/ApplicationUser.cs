using Microsoft.AspNetCore.Identity;

namespace WebApplication16.Models
{
    public class ApplicationUser:IdentityUser
    {
        // Add extra fields if needed

        // public string FirstName { get; set; }
        // public DateTime DateOfBirth { get; set; }
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
