using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ConsoleApp8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting program...\n");

            await GetMultipleDataAsync();

            Console.WriteLine("\nProgram finished.");
            Console.ReadLine();
        }


        public static async Task GetMultipleDataAsync()
        {
            Console.WriteLine("Calling both methods...\n");

            var task1 = GetUsersAsync();
            var task2 = GetOrdersAsync();

            await Task.WhenAll(task1, task2);

            var users = await task1;
            var orders = await task2;


            Console.WriteLine("\nUsers:");
            foreach (var user in users)
            {
                Console.WriteLine($"- {user}");
            }

            Console.WriteLine("\nOrders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"- {order}");
            }

        }


        public static async Task<List<string>> GetUsersAsync()
        {
            Console.WriteLine("Fetching users...");
            await Task.Delay(2000); // simulate delay

            return new List<string>
        {
            "Alice",
            "Bob",
            "Charlie"
        };
        }

        public static async Task<List<string>> GetOrdersAsync()
        {
            Console.WriteLine("Fetching orders...");
            await Task.Delay(3000); // simulate delay

            return new List<string>
        {
            "Order #101",
            "Order #102",
            "Order #103"
        };
        }
    }
}