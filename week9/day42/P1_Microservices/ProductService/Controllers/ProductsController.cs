using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product {Id = 1,Name="Laptop",Price=85000,Category="Electronics"},
            new Product {Id = 2,Name="IPhone",Price=95000,Category="Electronics"},
            new Product {Id = 3,Name="Tab",Price=55000,Category="Electronics"}
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = products.Find(item => item.Id == id);
            if (product == null)
                return NotFound("Product not found");
            else
                return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            products.Add(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var oldProduct = products.Find(item => item.Id == id);
            if (oldProduct == null)
                return NotFound("Product not found");
            else
            {
                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.Category = product.Category;
                return Ok(new { UpdateProduct = oldProduct, status = "Product details are updated sucessfully" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.Find(item => item.Id == id);
            if (product == null)
                return NotFound("Product not found");
            else
            {
                products.Remove(product);
                return Ok(new { product, status = "Product deleted sucess fully" });
            }
        }

    }
}
