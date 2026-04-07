using WebApplication11.Models;

namespace WebApplication8.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Course> GetCoursesWithStudents();
    }
}
