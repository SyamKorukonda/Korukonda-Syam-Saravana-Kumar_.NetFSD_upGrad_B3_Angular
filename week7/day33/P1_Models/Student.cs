using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models
{
    public class Student //child
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
         public Course Course { get; set; } // navigation 

    }
}
