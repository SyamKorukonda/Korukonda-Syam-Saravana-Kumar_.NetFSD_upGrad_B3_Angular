using System.ComponentModel.DataAnnotations;

namespace WebApplication17.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }

    public class ProductCreateUpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Product name must be at least 3 characters")]
        [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Description must be at least 5 characters")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be > 0")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }
    }
}
