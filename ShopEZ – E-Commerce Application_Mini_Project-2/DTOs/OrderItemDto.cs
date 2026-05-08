using System.ComponentModel.DataAnnotations;

namespace WebApplication17.DTOs
{
    public class OrderItemDto
    {
        [Required(ErrorMessage = "ProductId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0")]

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]

        public int Quantity { get; set; }
    }

    public class OrderItemResponseDto
    {
        public int ProductId { get; set; }

        [MinLength(3, ErrorMessage = "Product name is too short")]
        [MaxLength(200, ErrorMessage = "Product name is too long")]
        public string ProductName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]

        public decimal Price { get; set; }
    }
}
