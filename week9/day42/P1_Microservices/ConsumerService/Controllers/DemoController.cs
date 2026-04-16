using Consumer_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Consumer_Service.Models;

namespace Consumer_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IProductApiService _service;

        public DemoController(IProductApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _service.GetProducts());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var product = await _service.GetProductId(id);

            if (product == null)
                return NotFound("Product not found"); 

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
           
            return Ok(await _service.AddProduct(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("ID mismatch");

            return Ok(await _service.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            return Ok(await _service.DeleteProduct(id));
        }

    }
}
