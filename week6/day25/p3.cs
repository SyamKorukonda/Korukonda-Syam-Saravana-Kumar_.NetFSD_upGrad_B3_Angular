using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsApp
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Starting report generation...\n");

            // Run tasks concurrently
            Task t1 = Task.Run(() => GenerateSalesReport());
            Task t2 = Task.Run(() => GenerateInventoryReport());
            Task t3 = Task.Run(() => GenerateCustomerReport());

            // Wait for all tasks
            await Task.WhenAll(t1, t2, t3);

            Console.WriteLine("\nAll reports generated successfully!");
            Console.ReadLine();
        }

        // Method 1
        static void GenerateSalesReport()
        {
            Console.WriteLine("Sales Report Started...");
            Thread.Sleep(3000); // simulate work
            Console.WriteLine("Sales Report Completed!");
        }

        // Method 2
        static void GenerateInventoryReport()
        {
            Console.WriteLine("Inventory Report Started...");
            Thread.Sleep(2000);
            Console.WriteLine("Inventory Report Completed!");
        }

        // Method 3
        static void GenerateCustomerReport()
        {
            Console.WriteLine("Customer Report Started...");
            Thread.Sleep(2500);
            Console.WriteLine("Customer Report Completed!");
        }
    }
}