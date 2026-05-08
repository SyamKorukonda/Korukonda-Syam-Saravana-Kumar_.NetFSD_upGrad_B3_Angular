using System.ComponentModel.DataAnnotations;

namespace WebApplication17.DTOs
{
    public class OrderDto
    {
        [Required, MinLength(1, ErrorMessage = "Cart cannot be empty.")]
        public List<OrderItemDto> CartItems { get; set; } = new List<OrderItemDto>();
    }

    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new List<OrderItemResponseDto>();
    }
}
