using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Enter root directory path: ");
            string path = Console.ReadLine();

            // Check path
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid directory ");
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);

            // Get subdirectories
            DirectoryInfo[] dirs = di.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {
                Console.WriteLine("Folder: " + dir.Name);
                Console.WriteLine("Files: " + dir.GetFiles().Length);
                Console.WriteLine("----------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}