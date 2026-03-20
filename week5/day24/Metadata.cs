using System.IO;
using System.Numerics;


// File Info

namespace ConsoleApp8 
{
    class Program
    {
        static void Main()
        {
           // string filePath = "C:\\CSharp\\File1.txt";
            string filePath = @"D:\Csharp\basictext.txt"; // txt file


            // 1. FileInfo

            FileInfo fileInfo = new FileInfo(filePath);

            Console.WriteLine(fileInfo.Name); //basictext.txt
            Console.WriteLine(fileInfo.FullName);//D:\Csharp\basictext.txt
            Console.WriteLine(fileInfo.CreationTime.ToString()); //20 - 03 - 2026 10:51:52
            Console.WriteLine(fileInfo.Length);// it is empty =0,or if any code is  there it prints it length

            Console.WriteLine("------------------------------------");

            //2. DirectoryInfo
            DirectoryInfo dio = new DirectoryInfo(@"D:\Csharp\basic"); //in basic folder it is a directory

            Console.WriteLine("Creation Time    : {0}", dio.CreationTime.ToString());// time and date of the creation of diectory 20-03-2026 10:51:16
            Console.WriteLine("Parent folder    : {0}", dio.Parent);//D:\Csharp
            Console.WriteLine("Root  folder     : {0}", dio.Root);//D:\
            Console.WriteLine("No. of Files     : {0}", dio.GetFiles().Length); //.txt files  1
            Console.WriteLine("No. of Directories     : {0}", dio.GetDirectories().Length); // sub folders 2
          
            Console.WriteLine();


            Console.WriteLine("-----------------------------------------");
            // 3. DriveInfo 
            DriveInfo di = new DriveInfo("D:\\"); //complete d drive
            Console.WriteLine(di.DriveType);//  fixed 
            Console.WriteLine(di.TotalSize);//295696363520
            Console.WriteLine(di.AvailableFreeSpace);//208161361920
            Console.WriteLine();


            Console.ReadLine();


        }
    }

}
