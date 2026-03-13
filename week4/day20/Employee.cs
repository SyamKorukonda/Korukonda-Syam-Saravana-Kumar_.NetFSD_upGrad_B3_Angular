using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    internal class Employee
    {
        private string _fullName;
        private int _age;
        private decimal _salary;
        private readonly string _employeeId;
        //readonly is used to ensure that a field can be assigned only once
        //(during declaration or constructor) and cannot be modified later, which helps maintain data integrity.

        public string FullName
        {
            get =>_fullName;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Full name cannot be empty.");

                _fullName = value.Trim();
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                if (value < 18 || value > 80)
                    throw new ArgumentException("Age must be between 18 and 80.");

                _age = value;
            }
        }
        public decimal Salary
        {
            get => _salary;
            private set  //only the class itself can change salary
            {
                if (value < 1000)
                    throw new ArgumentException("Salary cannot be below 1000.");

                _salary = value;
            }
        }
        public string EmployeeId=>_employeeId;

        //constructor
        //o	Provide constructor(s) that force valid initial state.
        // At minimum, require: full name, starting salary, age

        public Employee(string fullName, decimal salary, int age, string employeeId = null)
        {
            _employeeId = employeeId;
            FullName = fullName;
            Age = age;
            Salary = salary;
        }

        //4.	Business Behavior (Public Methods – the only way to change salary) Implement controlled operations:
        //o	GiveRaise(decimal percentage)

        public void GiveRaise (decimal percentage)
        {
            if(percentage <= 0 || percentage > 30)
                throw new ArgumentException("Raise must be between 0 and 30 percent");
            Salary = Salary + (Salary * percentage / 100);
            Console.WriteLine("Salary increased. New Salary: " + Salary);
        }
        //o	DeductPenalty(decimal amount) (example of controlled decrease)
        //	Amount > 0
        //	After deduction, salary must remain ≥ 1000
        //		Return bool (true = success, false = failed due to policy violation)

        public bool DeductPenalty(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Penalty amount must be positive");

            if (Salary - amount < 1000)
                return false;

            Salary -= amount;

            Console.WriteLine("Penalty deducted. Salary: " + Salary);

            return true;
        }
    }
}
