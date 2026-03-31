using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("Feedback")]
    public class FeedbackController : Controller
    {
        [HttpGet("Index")] //show form
        public IActionResult Index()    
        {
            return View();
        }

        [HttpPost("Submit")]
        public IActionResult Submit(string name,string comments,int rating)
        {
            if (rating >= 4)
            {
                ViewData["Message"] = "Thank You for your feedback!";
            }
            else
            {
                ViewData["Message"] = "We will improve based on your feedback.";
            }

            ViewData["Name"] = name;

            return View("Index");
        }

    }
}
