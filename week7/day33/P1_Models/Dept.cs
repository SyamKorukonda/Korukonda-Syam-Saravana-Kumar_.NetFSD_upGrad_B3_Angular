namespace WebApplication8.Models
{
    public class Dept //parent class main class 
    {
        public int DeptId { get; set; }
        public string Dname { get; set; }
        public string Location { get; set; }
        public ICollection<Employee> Employees { get; set; } // Navigation Property
    }
}
