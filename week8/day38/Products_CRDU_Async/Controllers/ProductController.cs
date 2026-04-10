using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication12.Models;
using WebApplication12.Services;

namespace WebApplication12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _service.GetProductsAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound("Requested product does not exists");
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _service.CreateProductAsync(product);

            // Customize the response :  data, status codes 
            return Ok(new { product, status = "New product successfully added to server..!" });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Requested product Id mismatch.");
            }

            var result = await _service.UpdateProductAsync(product);

            if (result == null)
            {
                return NotFound("Requested product does not exists");
            }
            else
            {
                return Ok(new { updatedProduct = result, status = "Product details are updated successfully in server..!" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool falg = await _service.DeleteProductAsync(id);

            if (falg == false)
            {
                return NotFound("Requested product does not exists");
            }
            else
            {
                return Ok(new { status = "Product details are deleted successfully in server..!" });
            }
        }

    }
}
