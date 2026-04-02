using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;
        public StudentController(StudentDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //List of Students
            var stud = _context.Students.Include(s => s.Course).ToList();
            return View(stud);
        }
        //Add student
        //GET: Create Student
        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList(); // simple
            return View();
        }

        //POST: Create Student
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student.CourseId == 0)
            {
                ViewBag.Courses = _context.Courses.ToList();
                return View(student);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //list of course
        public IActionResult Course()
        {
            var cour=_context.Courses.Include(c=>c.Students).ToList();
            return View(cour);
        }
        // add course
        public IActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges(); 
            return RedirectToAction("Course");
        }
    }
}
