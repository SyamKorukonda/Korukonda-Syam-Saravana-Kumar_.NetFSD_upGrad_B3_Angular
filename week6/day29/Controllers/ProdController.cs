using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    public class ProdController : Controller
    {
        public static  List<Products> prodList = new List<Products>()
     {
         new Products{Pid=1,Pname="Laptop",Pprice=88000,Pcategory="Electronics"},
         new Products{Pid=2,Pname="Milk",Pprice=55,Pcategory="Food"},
         new Products{Pid=3,Pname="Mobile",Pprice=128000,Pcategory="Electronics"},
         new Products{Pid=4,Pname="Mango",Pprice=88,Pcategory="Fruits"},
         new Products{Pid=5,Pname="shirt",Pprice=8800,Pcategory="Cloths"},
     };
        public IActionResult Index()
        {
           
            return View(prodList);
        }

        public IActionResult Details( int id)
        {
            Products prodObj = prodList.FirstOrDefault(item => item.Pid == id);
            return View(prodObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Products prod)
        {
            if (ModelState.IsValid)
            {
                prodList.Add(prod);
                return RedirectToAction("Index");
            }
            return View(prod);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Products prod=prodList.FirstOrDefault(item => item.Pid == id);
            return View(prod);
        }
        [HttpPost]
        public IActionResult Update(Products prd)
        {
            if (ModelState.IsValid)
            {
                var exProd = prodList.FirstOrDefault(x => x.Pid == prd.Pid);
                exProd.Pname= prd.Pname;
                exProd.Pprice= prd.Pprice;
                exProd.Pcategory= prd.Pcategory;
                return RedirectToAction("Index");
            }
            return View(prd);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Products prodObj = prodList.FirstOrDefault(item => item.Pid == id);
            return View(prodObj);
        }

        [HttpPost]

        [ActionName("Delete")]      //  Mapping Delete Request to DeleteConfirm Action Method

        public IActionResult DeleteConform(int id)
        {
            Products prodObj = prodList.FirstOrDefault(item => item.Pid == id);
            prodList.Remove(prodObj);
            return RedirectToAction("Index");
        }


    }
}
