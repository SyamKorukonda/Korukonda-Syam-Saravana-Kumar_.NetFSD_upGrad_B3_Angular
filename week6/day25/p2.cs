using System;

namespace DiscountApp
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Discount Percentage: ");
            double discount = Convert.ToDouble(Console.ReadLine());

            // Breakpoint here
            double finalPrice = price - (price * discount / 100);

            Console.WriteLine("\nProduct: " + name);
            Console.WriteLine("Final Price: " + finalPrice);

            Console.ReadLine();
        }
    }
}