using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopEZ_ProductService.Common;
using ShopEZ_ProductService.Repositories;

namespace ShopEZ_ProductService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // ProductRepository to interact with Products table
        private readonly IProductRepository _repo;

        // Constructor — Dependency Injection
        public StockController(IProductRepository repo)
        {
            _repo = repo;
        }

        // PATCH api/products/{id}/reduce-stock?quantity=5
        [HttpPatch("{id}/reduce-stock")]
        public async Task<IActionResult> ReduceStock(int id, [FromQuery] int quantity)
        {
            try
            {
                // Validate product ID
                if (id <= 0)
                    return BadRequest(new ApiResponse<string>("Product ID must be greater than 0."));

                // Validate quantity
                if (quantity <= 0)
                    return BadRequest(new ApiResponse<string>("Quantity must be at least 1."));

                // Find product by ID
                var product = await _repo.GetByIdAsync(id);
                // Return 404 if product not found
                if (product == null)
                    return NotFound(new ApiResponse<string>($"Product with ID {id} was not found."));

                // Check if enough stock is available
                if (product.Stock < quantity)
                    return Conflict(new ApiResponse<string>(
                        $"Insufficient stock for '{product.Name}'. Available: {product.Stock}, Requested: {quantity}"));

                // Reduce stock
                product.Stock -= quantity;
                // Save updated stock to database
                await _repo.UpdateAsync(product);

                // Return 200 OK with remaining stock
                return Ok(new ApiResponse<string>($"Stock updated. Remaining: {product.Stock}", "Stock reduced successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }
    }
}
