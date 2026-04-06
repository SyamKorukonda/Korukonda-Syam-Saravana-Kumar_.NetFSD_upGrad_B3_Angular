using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models;
using WebApplication9.Services;

namespace WebApplication9.Controllers
{
    public class ProductsController : Controller
    {
       private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetProducts());
        }
        public IActionResult Details(int id)
        {
            var prodObj=_service.GetProduct(id);    
            return View(prodObj);
        }

        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]

        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                _service.CreateProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Invaild Product Details";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var prodObj=_service.GetProduct(id);
            return View(prodObj);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                _service.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Product details";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var prodObj= _service.GetProduct(id);
            return View(prodObj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConform(int id)
        {
            var prodObj = _service.GetProduct(id);

            if(prodObj != null)
            {
                _service.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Requested product does not exists";
                return View();
            }
        }
    }
}
