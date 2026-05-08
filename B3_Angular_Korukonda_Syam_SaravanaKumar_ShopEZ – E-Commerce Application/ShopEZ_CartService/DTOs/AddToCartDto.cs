using System.ComponentModel.DataAnnotations;

namespace ShopEZ_CartService.DTOs
{
    //  Add to Cart DTO — Incoming — validations required 
    // User only sends productId and quantity
    // ProductName, Price, Stock, ImageUrl are fetched from ProductService

    public class AddToCartDto
    {
        // ProductId must be valid
        [Required(ErrorMessage = "ProductId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
        public int ProductId { get; set; }

        // Quantity must be at least 1
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

    }

    //   Update Quantity DTO — Incoming — validations required 

    public class UpdateCartDto
    {
        // New quantity — must be at least 1
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    //  Response DTO — Outgoing — no validations needed 

    public class CartItemResponseDto
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        // Subtotal calculated from price and quantity
        public decimal Subtotal => Price * Quantity;
    }

    //  Cart Summary DTO — Outgoing 

    public class CartSummaryDto
    {
        public List<CartItemResponseDto> Items { get; set; } = new();
        // Total price of all items in cart
        public decimal TotalPrice { get; set; }
        // Total number of items in cart
        public int TotalItems { get; set; }
    }



    //  Product info fetched from ProductService via HTTP 

    public class ProductInfoDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
    }
}
