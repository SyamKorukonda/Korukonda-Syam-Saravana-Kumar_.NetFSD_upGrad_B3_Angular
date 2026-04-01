using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
namespace WebApplication6.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Find(id);
            return View(movie);
        }
        
        //Get
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if(ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = " Enter valid details ";
                return View(movie);
            }
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var movie = _context.Movies.Find(id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Update(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = " Enter valid details ";
                return View(movie);
            }
        }

        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            return View(movie);
        }
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConform(int id)
        {
            var movie= _context.Movies.Find(id);
            
            if (movie!=null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");   
            }
            else
            {
                ViewBag.ErrorMessage = "Requested movie does not exists";
                return View();
            }
        }
        


    }
}
