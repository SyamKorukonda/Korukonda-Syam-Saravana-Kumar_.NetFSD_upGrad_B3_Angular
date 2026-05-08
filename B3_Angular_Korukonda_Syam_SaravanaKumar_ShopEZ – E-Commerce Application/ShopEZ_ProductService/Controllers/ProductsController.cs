using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopEZ_ProductService.Common;
using ShopEZ_ProductService.DTOs;
using ShopEZ_ProductService.Services;

namespace ShopEZ_ProductService.Controllers
{
    // Defines the route for all endpoints -> api/products
    [Route("api/[controller]")]
    // Enables automatic model validation and API behaviors
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // ProductService handles all product business logic
        private readonly IProductService _productService;

        // Constructor — Dependency Injection
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products — Get all products (Public)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Fetch all products from ProductService
                var products = await _productService.GetAllAsync();
                // Return 200 OK with list of products
                return Ok(new ApiResponse<IEnumerable<ProductDto>>(products));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // GET api/products/{id} — Get single product by ID (Public)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // Fetch product by ID from ProductService
                var product = await _productService.GetByIdAsync(id);
                // Return 404 if product not found
                if (product == null)
                    return NotFound(new ApiResponse<string>("Product not found"));
                // Return 200 OK with product
                return Ok(new ApiResponse<ProductDto>(product));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // GET api/products/category/{category} — Get products by category (Public)
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            try
            {
                // Validate category input
                if (string.IsNullOrWhiteSpace(category))
                    return BadRequest(new ApiResponse<string>("Category is required"));

                // Fetch products by category from ProductService
                var products = await _productService.GetByCategoryAsync(category);

                // Return 404 if no products found
                if (products == null || !products.Any())
                    return NotFound(new ApiResponse<string>($"No products found for category '{category}'"));

                // Return 200 OK with product list
                return Ok(new ApiResponse<IEnumerable<ProductDto>>(products));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }



        // POST api/products — Create new product (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductCreateUpdateDto dto)
        {
            try
            {
                // Check if the incoming request data is valid based on DTO annotations
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                // Call ProductService to create new product
                var result = await _productService.CreateAsync(dto);
                // Return 200 OK with created product
                return Ok(new ApiResponse<ProductDto>(result, "Product created"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // PUT api/products/{id} — Update existing product (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateUpdateDto dto)
        {
            try
            {
                // Check if the incoming request data is valid based on DTO annotations
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                // Call ProductService to update product
                await _productService.UpdateAsync(id, dto);
                // Return 200 OK with success message
                return Ok(new ApiResponse<string>("OK", "Product updated successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }

        // DELETE api/products/{id} — Delete product (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Call ProductService to delete product
                await _productService.DeleteAsync(id);
                // Return 200 OK with success message
                return Ok(new ApiResponse<string>("OK", "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                // Return 500 for unexpected errors
                return StatusCode(500, new ApiResponse<string>(ex.Message));
            }
        }
    }
}