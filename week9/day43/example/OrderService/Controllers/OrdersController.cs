using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public OrdersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            string[] ordersArray = new string[] { "1001", "1002", "1003" };
            return Ok(ordersArray);
        }


        [HttpGet("{ProductId}")]

        public async Task<IActionResult> CreateOrder(int ProductId)
        {
            // Get product details based on product id
            // Perform Create Order 

            // here we create a response for the productService to the url 
            var response = await _httpClient.GetAsync($"https://localhost:7233/api/Products/{ProductId}");

            // if respose fails it returns it

            if (!response.IsSuccessStatusCode)
                return BadRequest("Product Not Found");
            //if it true it converts it in to json string

            var jsonData=await response.Content.ReadAsStringAsync();

            // it is used for case sencitive json use pascalCase ex:productId
            //but C# used CamelCase ex: ProductID so for this we use this

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            // It is used to converts JSON strings into C# objects
            // Use JsonSerializer.Deserialize<T>(jsonString) for direct conversion

            var productData =JsonSerializer.Deserialize<Product>(jsonData,options);

            var order = new
            {
                OrderId = 1,
                Product = productData
            };
            
            return Ok(order);
        }
    }
}
