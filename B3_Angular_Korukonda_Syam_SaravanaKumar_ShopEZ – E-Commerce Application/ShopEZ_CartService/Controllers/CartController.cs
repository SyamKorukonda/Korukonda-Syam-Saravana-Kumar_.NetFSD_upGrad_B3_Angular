using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopEZ_CartService.Common;
using ShopEZ_CartService.DTOs;
using ShopEZ_CartService.Services;

namespace ShopEZ_CartService.Controllers
{
    // Defines the route for all endpoints -> api/cart
    [Route("api/[controller]")]
    
    // Enables automatic model validation and API behaviors
    [ApiController]

    // All endpoints require authentication — only logged in users can access cart
    [Authorize]
    public class CartController : ControllerBase
    {
        // CartService handles all cart business logic
        private readonly ICartService _cartService;

        // Constructor — Dependency Injection
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET api/cart — Get all cart items for logged in user
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Fetch cart items for logged in user
                var cart = await _cartService.GetCartAsync(userId);
                // Return 200 OK with cart summary
                return Ok(new ApiResponse<CartSummaryDto>(cart, "Cart fetched successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // POST api/cart — Add product to cart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
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

                // Call CartService to add product to cart
                var result = await _cartService.AddToCartAsync(userId, dto);
                // Return 200 OK with added cart item
                return Ok(new ApiResponse<CartItemResponseDto>(result, "Product added to cart successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }


        // PUT api/cart/{cartItemId} — Update quantity of cart item
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [FromBody] UpdateCartDto dto)
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

                // Call CartService to update quantity
                var result = await _cartService.UpdateQuantityAsync(userId, cartItemId, dto);
                // Return 200 OK with updated cart item
                return Ok(new ApiResponse<CartItemResponseDto>(result, "Quantity updated successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }


        // DELETE api/cart/{cartItemId} — Remove item from cart
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Call CartService to remove item from cart
                await _cartService.RemoveFromCartAsync(userId, cartItemId);
                // Return 200 OK with success message
                return Ok(new ApiResponse<string>("Removed", "Product removed from cart successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // DELETE api/cart/clear — Clear all cart items for logged in user
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                // Extract UserId from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized(new ApiResponse<string>("Invalid or missing token."));

                // Call CartService to clear all cart items
                await _cartService.ClearCartAsync(userId);
                // Return 200 OK with success message
                return Ok(new ApiResponse<string>("Cleared", "Cart cleared successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

    }
}
