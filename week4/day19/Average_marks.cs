
/*
A school wants to calculate the average marks of a student using a class-based approach.
Requirements:
1. Create a class Student.
2. Create method CalculateAverage(int m1, int m2, int m3).
3. Return the average marks.
4. Display grade based on average.

*/
namespace ConsoleApp16
{

    class Student
    {
        public double Average(int m1,int m2, int m3)
        {
            return (m1 + m2+ m3)/3;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student();
            double avg=s.Average(80,90,70);
            if (avg >= 80)
                Console.WriteLine($"Average = {avg}, Grade= A ");
            else if(avg <80 && avg>= 70)
                Console.WriteLine($"Average = {avg}, Grade= B ");
            else if(avg <70 && avg >=60)
                Console.WriteLine($"Average = {avg}, Grade= C ");
            else if(avg <60 && avg >=45)
                Console.WriteLine($"Average = {avg}, Grade= D ");
            else
                Console.WriteLine($"Average = {avg}, Grade= F ");

            Console.ReadLine(); 
        }
    }
}
