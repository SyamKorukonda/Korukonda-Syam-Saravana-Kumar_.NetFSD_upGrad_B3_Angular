using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("Calc")]
    public class CalcController : Controller
    {
        [HttpGet("add")] // get data 
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("add")]
        public IActionResult Add(int num1,int num2)
        {
            int result=num1 + num2;
            ViewData["Result"]=result;
            ViewData["num1"] = num1;
            ViewData["num2"]=num2;
            return View();
        }
    }
}
