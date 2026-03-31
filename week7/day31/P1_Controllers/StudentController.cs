using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("Student")]
    public class StudentController : Controller
    {
        // Get ,Display form
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        // Post ,Handle the submission 
        [HttpPost("submit")]
        public IActionResult Submit(string name,int age,string course)
        {
            ViewBag.Name = name;    
            ViewBag.Age = age;  
            ViewBag.Course = course;
            return RedirectToAction("Details", new {name,age,course});
        }

        // Get ,display data
        public IActionResult Details(string name,int age,string course)
        {
            ViewBag.Name = name;    
            ViewBag.Age = age;
            ViewBag.Course= course;
            return View();
        }
    }
}
