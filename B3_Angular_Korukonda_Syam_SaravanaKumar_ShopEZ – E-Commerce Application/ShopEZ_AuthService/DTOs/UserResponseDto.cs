using System.ComponentModel.DataAnnotations;

namespace ShopEZ_AuthService.DTOs
{
    // Responses 
    // Outgoing — no validations needed
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
