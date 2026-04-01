using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    public class ProductsController : Controller
    {
        /* private readonly ApplicationDbContext _context;

         public ProductsController(ApplicationDbContext context) //constructor 
         {
               _context=context;
         }
         public IActionResult Index()
         {
             var products = _context.products.ToList();  // Read
             return View(products);

         }

         public IActionResult Details(int id)
         {
             var prodObj = _context.products.Find(id);
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
                 _context.products.Add(product); //create
                 _context.SaveChanges();//// Update to Database  
                 return RedirectToAction("Index");
             }
             else
             {
                 ViewBag.ErrorMessage = "Invalid Product details.";
                 return View();
             }
         }

         [HttpGet]
         public IActionResult  Edit(int id)
         {
             var prodObj = _context.products.Find(id);
             return View(prodObj);
         }
         [HttpPost]
         public IActionResult Edit(Product product)
         {
             if (ModelState.IsValid)
             {
                 _context.products.Update(product); //update
                 _context.SaveChanges();//// Update to Database  
                 return RedirectToAction("Index");
             }
             else
             {
                 ViewBag.ErrorMessage = "Invalid Product details.";
                 return View();
             }
         }

         [HttpGet]
         public IActionResult Delete(int id)
         {
             var prodObj = _context.products.Find(id);
             return View(prodObj);
         }

         [HttpPost]
         [ActionName("Delete")]
         public IActionResult DeleteConform(int id)
         {
             var prodObj = _context.products.Find(id);

             if (prodObj != null)
             {
                 _context.products.Remove(prodObj);
                 _context.SaveChanges();
                 return RedirectToAction("Index");
             }
             else
             {
                 ViewBag.ErrorMessage = "Requested product does not exists";
                 return View();
             }
         }

       //   use all these for if you just want a entity Framework core 
 //-------------------------------------------------------------------------------------------------------------------
  //      if uou want the Dependency Injection ,Service-Repository pattern use the bellow code 

         */

        //Dependency Injection ,Service-Repository patter

        private readonly IProductService _service;
        
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var products = _service.GetProducts();
            return View(products);

            //or use this directly  : return View(_service.GetProducts());
        }

        public IActionResult Details(int id)
        {
            var probObj=_service.GetProduct(id);
            return View(probObj);
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
                ViewBag.ErrorMessage = "Invalid Product details";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var prodObj= _service.GetProduct(id);
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
                ViewBag.ErrorMessage = "Invalid Product Details";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var prodObj=_service.GetProduct(id);
            return View(prodObj);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConform(int id)
        {
            var pordObj= _service.GetProduct(id);

            if(pordObj != null)
            {
                _service.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Product does not exists";
                return View();
            }
        }








    }
}
