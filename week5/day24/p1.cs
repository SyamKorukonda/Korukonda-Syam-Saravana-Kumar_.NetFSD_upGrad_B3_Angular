using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp41
{

    class Program
    {
        static void Main()
        {
            string dire = "logs";
            if (Directory.Exists(dire) == false) 
            { 
                Directory.CreateDirectory(dire);
            }
            string logfilePath = Path.Combine(dire, "p1_log.txt");

            try
            {
                Console.Write(" enter message:  ");
                string msg = Console.ReadLine();

                using (StreamWriter sw = new StreamWriter(logfilePath, true))
                {
                    sw.WriteLine(msg);
                }
                Console.WriteLine("Message is saved");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:"+ex.Message);
            }




            Console.ReadLine();

        }
    }
}
