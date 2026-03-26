using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class EmpController : Controller
    {
        public IActionResult Index()
        {
            //Employee empObj = new Employee() { Empno = 1, Ename = "Syam", Job = "AppDevelouper", Salary = 66000, Deptno = 1 };

            List<Employee> empList = new List<Employee>()
            {
                new Employee() { Empno = 1, Ename = "Syam", Job = "AppDevelouper", Salary = 66000, Deptno = 1},
                new Employee() {Empno = 2, Ename = "Kumar", Job = "AppDevelouper", Salary = 65000, Deptno = 1 },
                new Employee() {Empno = 3, Ename = "sudheer", Job = "Analyst", Salary = 65000, Deptno = 3 },
                new Employee() {Empno = 4, Ename = "hari", Job = "BackEnd", Salary = 65000, Deptno = 2 },
                new Employee() { Empno = 2, Ename = "Ravi", Job = "Tester", Salary = 45000, Deptno = 4 }
            };


            return View(empList);
        }
    }
}
