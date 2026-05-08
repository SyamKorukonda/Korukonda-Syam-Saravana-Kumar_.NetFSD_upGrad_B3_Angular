using System.ComponentModel.DataAnnotations;

namespace ShopEZ_OrderService.DTOs
{
    //  Create Order DTO — Incoming — direct order from product 

    public class OrderDto
    {
        // Cart items — sent directly from frontend for Buy Now
        [Required(ErrorMessage = "Cart items are required")]
        [MinLength(1, ErrorMessage = "Cart cannot be empty")]
        public List<OrderItemDto> CartItems { get; set; } = new();
    }

    public class OrderItemDto
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

    //  Response DTOs — Outgoing — no validations needed 

    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Shows if order is cancelled
        public bool IsCancelled { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }

    public class OrderItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        // Subtotal calculated from price and quantity
        public decimal Subtotal => Price * Quantity;
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

    //  Cart info fetched from CartService via HTTP 

    public class CartSummaryDto
    {
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
    }

    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
    }
}