using System.ComponentModel.DataAnnotations;

namespace WebApplication17.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required,MinLength(3),MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required,MaxLength(50)]
        public string Role { get; set; } = "Customer"; // Admin / Customer

        //Navigation Property
        public ICollection<Order> Orders { get; set; }=new List<Order>();   

    }
}
