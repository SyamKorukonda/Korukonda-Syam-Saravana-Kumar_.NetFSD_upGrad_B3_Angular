using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Services;
namespace WebApplication6.Controllers
{
    public class MovieController : Controller
    {
        /*
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
         // this is ApplicatinDbContext ;Entity framework 

         // below is the EF Dependency Injection Service Repository Pattern

        */

        private readonly IMovieService _service;
        public MovieController(IMovieService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetMovies());
        }

        public IActionResult Details(int id)
        {
            return View(_service.GetMovie(id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if(ModelState.IsValid)
            {
                _service.CreateMovie(movie);
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = " Enter valid details ";
            return View(movie);

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_service.GetMovie(id));
        }
        [HttpPost]
        public IActionResult Update(Movie movie)
        {
            if(ModelState.IsValid)
            {
                _service.UpdateMovie(movie);
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
            return View(_service.GetMovie(id));
        }
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConform(int id)
        {
            var movie= _service.GetMovie(id);
            if(movie == null)
            {
                _service.DeleteMovie(id);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = " Enter valid details ";
                return View(movie);
            }
        }
        


    }
}
