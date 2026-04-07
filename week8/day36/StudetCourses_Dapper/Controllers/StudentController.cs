using Microsoft.AspNetCore.Mvc;
using WebApplication8.Repositories;

namespace WebApplication11.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repo;
        public StudentController(IStudentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StudentsWithCourse()
        {
            var data = _repo.GetStudentsWithCourses();  
            return View(data);
        }

        public IActionResult CoursesWithStudents()
        {
            var data = _repo.GetCoursesWithStudents();
            return View(data);
        }
    }
}
