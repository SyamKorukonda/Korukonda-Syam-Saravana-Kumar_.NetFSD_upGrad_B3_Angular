
// Write a C# program to create a Student class by using Enum variables as members of the class.


namespace ConsoleApp8
{
    // declares the enum
    enum StudentGrades
    {
        Excellent,
        Good,
        Average,
        Poor
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentGrades Grade { get; set; }
    }

    class Program
    {
        static void Main()
        {
            Student obj = new Student();
            obj.Id = 1023;
            obj.Name = "Scott";
            obj.Grade = StudentGrades.Good;


            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Id: ", obj.Id);
            Console.WriteLine("Name: ", obj.Name);
            Console.WriteLine("Grade: ", obj.Grade);

            Console.ReadLine();
        }
    }

}
