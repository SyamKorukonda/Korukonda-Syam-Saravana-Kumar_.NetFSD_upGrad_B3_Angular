using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            // Get all drives
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                // Check if drive is ready
                if (!drive.IsReady)
                    continue;

                Console.WriteLine("Drive: " + drive.Name);
                Console.WriteLine("Type: " + drive.DriveType);

                long total = drive.TotalSize;
                long free = drive.AvailableFreeSpace;

                Console.WriteLine("Total Size: " + total + " bytes");
                Console.WriteLine("Free Space: " + free + " bytes");

                // Calculate free percentage
                double freePercent = (free * 100.0) / total;

                Console.WriteLine("Free %: " + freePercent.ToString("0.00") + "%");

                // Warning
                if (freePercent < 15)
                {
                    Console.WriteLine("Warning: Low disk space!");
                }

                Console.WriteLine("---------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}