namespace ConsoleApp8
{

    public record Student(int RollNo, string Name, string Course, int Marks);

class Program
{
    static void Main()
    {
        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());

        Student[] students = new Student[n];

        // Input
        for (int i = 0; i < n; i++)
        {
            Console.Write("Enter Roll No: ");
            int roll = int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Course: ");
            string course = Console.ReadLine();

            Console.Write("Enter Marks: ");
            int marks = int.Parse(Console.ReadLine());

            students[i] = new Student(roll, name, course, marks);
        }

        // Display
        Console.WriteLine("\nStudent Records:");
        foreach (var s in students)
        {
            Console.WriteLine($"Roll No: {s.RollNo} | Name: {s.Name} | Course: {s.Course} | Marks: {s.Marks}");
        }

        // Search
        Console.Write("\nEnter Roll No to search: ");
        int search = int.Parse(Console.ReadLine());

        bool found = false;

        foreach (var s in students)
        {
            if (s.RollNo == search)
            {
                Console.WriteLine("\nStudent Found:");
                Console.WriteLine($"Roll No: {s.RollNo} | Name: {s.Name} | Course: {s.Course} | Marks: {s.Marks}");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Student not found.");
        }
    }
}

}
