using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.ApiResponse;
using WebApplication17.DTOs;
using WebApplication17.Services;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")] //route → api/Products
    [ApiController] // Enables API-specific features like automatic validation
    public class ProductsController : ControllerBase
    {
        // Inject ProductService
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        // Endpoint: GET api/Products
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        // Endpoint: GET api/Products/{id}
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse<string>("Product not found"));

            return Ok(new ApiResponse<ProductDto>(product));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only Admins can add products
        // Endpoint: POST api/Products
        public async Task<IActionResult> Create([FromBody] ProductCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.CreateAsync(dto);
            return Ok(new ApiResponse<ProductDto>(result, "Product created"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only Admins can Update the Product 
                                     // Endpoint: PUT api/Products/{id}
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productService.UpdateAsync(id, dto);
            return Ok(new ApiResponse<string>("OK", "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] //Only Admins can delete the product
                                     // Endpoint: DELETE api/Products/{id}
        public async Task<IActionResult> Delete(int id)
        {

            await _productService.DeleteAsync(id);
            return Ok(new ApiResponse<string>("OK", "Product Deleted successfully"));
        }
    }
}
