using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Net.Http;
using System.Text.Json;  // HttpClient class 

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        public OrdersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        [HttpGet("{productId}")]
        public async Task<IActionResult> CreateOrder(int productId)
        {
            // Get product details based on product id
            // Perform Create Order 

            var response = await _httpClient.GetAsync($"https://localhost:7200/api/Products/{productId}");

            
            if (!response.IsSuccessStatusCode)
                return BadRequest("Product not found");


            var jsonData = await response.Content.ReadAsStringAsync();


            // It converts JSON strings into C# objects
            // Use JsonSerializer.Deserialize<T>(jsonString) for direct conversion

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var productData  = JsonSerializer.Deserialize<Product>(jsonData, options); 
            
            var order = new
            {
                OrderId = 1,
                Product = productData
            };

            return Ok(order);
        }

    }
}
