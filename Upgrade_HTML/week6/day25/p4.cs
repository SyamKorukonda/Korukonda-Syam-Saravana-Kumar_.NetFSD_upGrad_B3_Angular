using System;
using System.Threading.Tasks;

namespace OrderProcessingApp
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Order Processing Started...\n");

            await ProcessOrderAsync();

            Console.WriteLine("\nOrder Processing Completed!");
            Console.ReadLine();
        }

        static async Task ProcessOrderAsync()
        {
            await VerifyPaymentAsync(); //await is used to wait for an asynchronous task to complete
                                        //without blocking the thread
            await CheckInventoryAsync();
            await ConfirmOrderAsync();
        }

        static async Task VerifyPaymentAsync()
        {
            Console.WriteLine("Verifying Payment...");
            await Task.Delay(2000);
            Console.WriteLine("Payment Verified \n");
        }

        static async Task CheckInventoryAsync()
        {
            Console.WriteLine("Checking Inventory...");
            await Task.Delay(1500);
            Console.WriteLine("Inventory Available \n");
        }

        static async Task ConfirmOrderAsync()
        {
            Console.WriteLine("Confirming Order...");
            await Task.Delay(1000);
            Console.WriteLine("Order Confirmed \n");
        }
    }
}