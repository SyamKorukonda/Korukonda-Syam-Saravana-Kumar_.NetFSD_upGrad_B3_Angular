using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication17.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        //Navigation Property

        public User ?User { get; set; } // ? Avoid null warnings,EF Core handles it
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
