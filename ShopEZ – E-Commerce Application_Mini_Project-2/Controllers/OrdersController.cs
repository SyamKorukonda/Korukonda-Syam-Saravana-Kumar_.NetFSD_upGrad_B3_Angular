using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.ApiResponse;
using WebApplication17.DTOs;
using WebApplication17.Models;
using WebApplication17.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost] // Place/Create the Order 
               // Endpoint: POST api/Orders

    public async Task<IActionResult> PlaceOrder([FromBody] OrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Extract UserId from JWT token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("Invalid token");

        // Convert UserId from string → int
        var userId = int.Parse(userIdClaim.Value);

        var result = await _orderService.CreateOrderAsync(userId, dto);

        return Ok(new ApiResponse<OrderResponseDto>(result, "Order placed successfully"));
    }

    [HttpGet("my-orders")] // Get Customer Orders
    // Endpoint: GET api/Orders/my-orders

    public async Task<IActionResult> GetMyOrders()
    {
        // Extract UserId from JWT token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        // Fetch orders belonging to logged-in user
        var orders = await _orderService.GetMyOrdersAsync(userId);

        return Ok(new ApiResponse<IEnumerable<OrderResponseDto>>(orders));
    }

    [HttpGet("all-orders")] //Get All Customers  Orders 
    [Authorize(Roles = "Admin")]  // only Admins can Acess this 
    // Endpoint: GET api/Orders/all-orders

    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(new ApiResponse<IEnumerable<OrderResponseDto>>(orders));
    }

    //Get Order By ID (Secure)
    [HttpGet("{id}")]
    // Endpoint: GET api/Orders/{id}
    public async Task<IActionResult> GetOrderById(int id)
    {
        // Extract UserId from JWT token
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var order = await _orderService.GetOrderByIdAsync(id);

        if (order == null)
            return NotFound(new ApiResponse<string>("Order not found with the order id  "));

        if (order.UserId != userId && !User.IsInRole("Admin"))
            return StatusCode(403, new ApiResponse<string>("You are not allowed to access this order only Authorize Custormer or Admin can Access"));

        //For consistent API responses, I replace Forbid() with a custom 403 response using a standardized ApiResponse wrapper.

        return Ok(new ApiResponse<OrderResponseDto>(order));
    }

}