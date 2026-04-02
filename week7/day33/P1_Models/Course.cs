namespace WebApplication8.Models
{
    public class Course //parent 
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public List<Student> Students { get; set; } // navigation 
    }
}
