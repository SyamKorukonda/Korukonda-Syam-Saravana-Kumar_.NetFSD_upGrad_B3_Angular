using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;


namespace ConsoleApp8
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Loading...");
            string result = await GetUsersAsync();
            Console.WriteLine("Reading data from API is Done!");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static async Task<string> GetUsersAsync()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/users");
            return response;
        }
    }
}