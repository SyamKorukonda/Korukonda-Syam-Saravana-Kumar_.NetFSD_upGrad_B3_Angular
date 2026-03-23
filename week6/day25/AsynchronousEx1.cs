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
            await LoadDataAsync();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static async Task LoadDataAsync()
        {
            await Task.Delay(5000);
            Console.WriteLine("Loading Data from the soruce is completed");
            // Load the data from file or api
        }
    }
}