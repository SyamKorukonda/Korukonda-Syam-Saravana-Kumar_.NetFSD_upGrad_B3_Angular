using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Enter Employe Name: ");
        String name = Console.ReadLine();
        Console.Write("Enter Employe sales Amount: ");
        Double sales = Double.Parse(Console.ReadLine());
        Console.Write("Enter Rating 1-5 : ");
        int rating = int.Parse(Console.ReadLine());

        // getting tuple, Tuple Deconstruction of a Value Tuple
        var (s, r) = GetPerformaceData(sales, rating);

        string performance = (s, r) switch
        {
            ( >= 100000, >= 4) => "High Performer",
            ( >= 50000, >= 3) => "Average Performer",
            _ => "Needs Improvement"
        };

        Console.WriteLine("\nEmployee Name: " + name);
        Console.WriteLine("Sales Amount: " + s);
        Console.WriteLine("Rating: " + r);
        Console.WriteLine("Performance: " + performance);



        Console.ReadLine();
    }

    static (double,int) GetPerformaceData(double sales,int rating)
    {
        return (sales,rating);
    }
}