using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Enter folder path: ");
            string path = Console.ReadLine();

            // Check directory
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid folder path ");
                return;
            }

            // Get files
            DirectoryInfo di = new DirectoryInfo(path);
            Console.WriteLine("\nFolder Name: " + di.Name);
            Console.WriteLine("Full Path: " + di.FullName);
            Console.WriteLine("------------------------");
            FileInfo[] files = di.GetFiles();

            int count = 0;

            foreach (FileInfo fi in files)
            {
                Console.WriteLine("Name: " + fi.Name);
                Console.WriteLine("Size: " + fi.Length + " bytes");
                Console.WriteLine("Created: " + fi.CreationTime);
                Console.WriteLine("----------------------");

                count++;
            }

            Console.WriteLine("Total files: " + count);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}