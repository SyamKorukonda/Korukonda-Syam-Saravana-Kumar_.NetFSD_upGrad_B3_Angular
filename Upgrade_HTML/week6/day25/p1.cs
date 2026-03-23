using System;
using System.Threading.Tasks;

namespace AsyncLoggerApp
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Application Started...");

            // Calling async logging multiple times
            Task t1 = WriteLogAsync("User logged in");
            Task t2 = WriteLogAsync("File uploaded");
            Task t3 = WriteLogAsync("Data processed");

            Console.WriteLine("Main thread is free...");

            // Wait for all logs to complete
            await Task.WhenAll(t1, t2, t3); //Used to run multiple tasks concurrently
                                            //(at the same time) and wait for all of them to finish

            Console.WriteLine("All logs written!");
            Console.ReadLine();
        }

        public static async Task WriteLogAsync(string message)
        {
            Console.WriteLine($"Start writing log: {message}");

            // Simulate file writing delay
            await Task.Delay(2000);

            Console.WriteLine($"Log written: {message}");
        }
    }
}