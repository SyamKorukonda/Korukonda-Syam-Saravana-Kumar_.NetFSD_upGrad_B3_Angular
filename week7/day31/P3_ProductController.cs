using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        [HttpGet("Index")] //show page
        public IActionResult Index()
        {
            //HttpContext.Session.GetString("key"); products : key  ,
            //you can use any other name but same name in both storing and retriving 
            var products = HttpContext.Session.GetString("products");//Used to retrieve (read) data from Session
            ViewBag.Products = products;
            return View();
        }
        [HttpPost("Add")] //Add product
        public IActionResult Add(string name,decimal price,int quantity)
        {
            string existing = HttpContext.Session.GetString("products");
            string newProduct=$"{name},{price},{quantity}";
            if (string.IsNullOrEmpty(existing))
                existing = newProduct;
            else
                existing += "|" + newProduct;
            HttpContext.Session.SetString("products", existing);
            return RedirectToAction("Index");
        }
    }
}
