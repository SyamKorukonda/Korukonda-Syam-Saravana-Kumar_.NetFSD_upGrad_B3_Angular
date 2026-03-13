namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //public Employee(string fullName, decimal salary, int age, string employeeId = null)

            Employee emp = new Employee( "Skumar", 55000, 23,"E101");

            Console.WriteLine(emp.EmployeeId);
            Console.WriteLine(emp.Salary);

            emp.GiveRaise(15);

            emp.DeductPenalty(500);

            emp.FullName = "S kumar.";

            Console.WriteLine(emp.FullName);

            Console.ReadLine(); 
        }
    }
}
