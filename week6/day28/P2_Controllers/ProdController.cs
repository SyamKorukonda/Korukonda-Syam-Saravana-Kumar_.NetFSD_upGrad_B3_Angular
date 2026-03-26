using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    public class ProdController : Controller
    {
        public IActionResult Index()
        {
            List<Products> prodList = new List<Products>()
     {
         new Products{Pid=1,Pname="Laptop",Pprice=88000,Pcategory="Electronics"},
         new Products{Pid=2,Pname="Milk",Pprice=55,Pcategory="Food"},
         new Products{Pid=3,Pname="Mobile",Pprice=128000,Pcategory="Electronics"},
         new Products{Pid=4,Pname="Mango",Pprice=88,Pcategory="Fruits"},
         new Products{Pid=5,Pname="shirt",Pprice=8800,Pcategory="Cloths"},
     };

            return View(prodList);
        }

        public IActionResult Details()
        {
            Products prodObj = new Products() { Pid = 1, Pname = "Laptaop", Pprice = 88000, Pcategory = "Electronics" };
            return View(prodObj);
        }
    }
}
