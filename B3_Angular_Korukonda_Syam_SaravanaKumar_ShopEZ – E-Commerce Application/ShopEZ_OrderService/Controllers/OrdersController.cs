using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopEZ_OrderService.Common;
using ShopEZ_OrderService.DTOs;
using ShopEZ_OrderService.Services;

namespace ShopEZ_OrderService.Controllers
{
    // Defines the route for all endpoints -> api/orders
    [Route("api/[controller]")]
    // Enables automatic model validation and API behaviors
    [ApiController]
    // All endpoints require authentication
    [Authorize]
    public class OrdersController : ControllerBase
    {
        // OrderService handles all order business logic
        private readonly IOrderService _orderService;

        // Constructor — Dependency Injection
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST api/orders — Approach 1: Direct order from product (Buy Now)
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto dto)
        {
            try
            {
                // Check if the incoming request data is valid based on DTO annotations
                if (!ModelState.IsValid)
                {
                    // Collect all validation error messages and join them
                    var errors = string.Join(", ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return BadRequest(new ApiResponse<string>($"Validation failed: {errors}"));
                }

                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Call OrderService to create order directly from product
                var result = await _orderService.CreateOrderAsync(userId, dto);
                // Return 200 OK with created order
                return Ok(new ApiResponse<OrderResponseDto>(result, "Order placed successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // POST api/orders/from-cart — Approach 2: Order all items from cart (Checkout)
        [HttpPost("from-cart")]
        public async Task<IActionResult> PlaceOrderFromCart()
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Get JWT token from Authorization header — forwarded to CartService
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Call OrderService to create order from cart
                // CartService is called automatically — no body needed
                var result = await _orderService.CreateOrderFromCartAsync(userId, token);
                // Return 200 OK with created order
                return Ok(new ApiResponse<OrderResponseDto>(result, "Order placed from cart successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // GET api/orders/my-orders — Get logged in user orders
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Fetch orders for logged in user
                var orders = await _orderService.GetMyOrdersAsync(userId);
                // Return 200 OK with list of orders
                return Ok(new ApiResponse<IEnumerable<OrderResponseDto>>(orders, "Orders fetched successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // GET api/orders/all-orders — Get all orders (Admin only)
        [HttpGet("all-orders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                // Fetch all orders from OrderService
                var orders = await _orderService.GetAllOrdersAsync();
                // Return 200 OK with list of all orders
                return Ok(new ApiResponse<IEnumerable<OrderResponseDto>>(orders, "All orders fetched successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // GET api/orders/{id} — Get single order by ID (Owner or Admin)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Fetch order by ID from OrderService
                var order = await _orderService.GetOrderByIdAsync(id);
                // Return 404 if order not found
                if (order == null)
                    return NotFound(new ApiResponse<string>("Order not found"));

                // Only the order owner or Admin can view the order
                bool isAdmin = User.IsInRole("Admin");
                if (order.UserId != userId && !isAdmin)
                    return StatusCode(403, new ApiResponse<string>("Access denied. You can only view your own orders."));

                // Return 200 OK with order
                return Ok(new ApiResponse<OrderResponseDto>(order, "Order fetched successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // PATCH api/orders/{id}/cancel — Cancel order (Owner or Admin)
        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Check if the logged in user is Admin
                bool isAdmin = User.IsInRole("Admin");

                // Call OrderService to cancel the order
                await _orderService.CancelOrderAsync(id, userId, isAdmin);

                // Return 200 OK with success message
                return Ok(new ApiResponse<string>("Cancelled", "Order cancelled successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }
    }
}