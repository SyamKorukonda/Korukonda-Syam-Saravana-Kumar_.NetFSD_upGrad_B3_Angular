using System.ComponentModel.DataAnnotations;

namespace WebApplication17.DTOs
{
    public class RegisterDto
    {
        [Required,MinLength(3),MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression("Admin|Customer", ErrorMessage = "Role must be Admin or Customer")]
        public string Role { get; set; } // "Admin" or "User"
    }
}
