using System;

namespace ConsoleApp35
{
    internal class Program
    {

        static void Greeting()
        {
            Console.WriteLine("Welcome to C# Methods");
        }

        static void Greeting(string uname)
        {
            Console.WriteLine($"Hi {uname}, Good morning...!");
        }

        static int GetSum(int x, int y)
        {
            int z = x + y;
            return z;
        }


        static string GetCurrentTime()
        {
            string str = DateTime.Now.ToString("T");
            return str;
        }



        /*
            Full date & time	DateTime.Now
            Date only	        DateTime.Now.Date
            Time only	        DateTime.Now.TimeOfDay
            Day	                DateTime.Now.Day
            Month	            DateTime.Now.Month
            Year	            DateTime.Now.Year
            Hour	            DateTime.Now.Hour
            Minute	            DateTime.Now.Minute
            Second	            DateTime.Now.Second
            Day of week	        DateTime.Now.DayOfWeek

            "t"	Short time	    09:52
            "T"	Long time	    09:52:30
            "d"	Short date	    12-03-2026
            "D"	Long date	    Thursday, March 12, 2026

            "f"	            Full date + short time
            "F"	            Full date + long time
            "g"	            General date + short time
            "G"	            General date + long time
            "M"	            Month day
            "Y"	            Month year
         
         */


        static void Main(string[] args)
        {

            Greeting();
            Greeting();
            Console.WriteLine("---------------------------");

            Greeting("Narasimha");
            Greeting("Scott");
            Console.WriteLine("---------------------------");

            Console.WriteLine("Sum Result : " + GetSum(10, 20));
            Console.WriteLine("Sum Result : " + GetSum(402, 503));

            Console.WriteLine("---------------------------");

            Console.WriteLine("Current Time : " + GetCurrentTime());

            Console.ReadLine();
        }
    }
}
