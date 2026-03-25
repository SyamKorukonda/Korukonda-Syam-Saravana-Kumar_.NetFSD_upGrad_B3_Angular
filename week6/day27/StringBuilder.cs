using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace ConsoleApp41
{
    class Program
    {
        static string conStr;

        static void Main()
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("[Using  StringBuilder]");
            Console.WriteLine("Start Time {0}", DateTime.Now.ToString("T"));
            for(int i = 0; i < 10; i++)
            {
                sb.Append(i);
            }
            Console.WriteLine("End Time {0}", DateTime.Now.ToString("T"));

            Console.WriteLine();

            string s = "";
            Console.WriteLine("[Using  String]");
            Console.WriteLine("Start Time {0}", DateTime.Now.ToString("T"));
            for (int i = 0; i < 10; i++)
            {
                s=s+i;
            }
            Console.WriteLine("End Time {0}", DateTime.Now.ToString("T"));


            Console.ReadLine(); 
        }
    }
}