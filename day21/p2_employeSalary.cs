using System;

namespace EmployeeSalarySystem
{
    // Base Class
    class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }

        // Virtual Method
        public virtual double CalculateSalary() => BaseSalary;


        // Derived Class Manager
        class Manager : Employee
        {
            public override double CalculateSalary() => BaseSalary + (BaseSalary * 0.20);


            // Derived Class Developer
            class Developer : Employee
            {
                public override double CalculateSalary() => BaseSalary + (BaseSalary * 0.10);


                class Program
                {
                    static void Main(string[] args)
                    {
                        Console.Write("enter Manager Name:");
                        String manager = Console.ReadLine();
                        Console.Write("enter Developer Name:");
                        String developer = Console.ReadLine();

                        Console.WriteLine("----------------------");

                        Employee emp;

                        // Manager object
                        emp = new Manager();
                        emp.Name = manager;
                        emp.BaseSalary = 50000;
                        //Console.WriteLine("Manager Salary = " + emp.CalculateSalary());
                        Console.WriteLine($"Manager : {emp.Name}, Salary ={emp.CalculateSalary()}");

                        // Developer object
                        emp = new Developer();
                        emp.Name = developer;
                        emp.BaseSalary = 50000;
                        Console.WriteLine($"Developer : {emp.Name}, Salary ={emp.CalculateSalary()}");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}