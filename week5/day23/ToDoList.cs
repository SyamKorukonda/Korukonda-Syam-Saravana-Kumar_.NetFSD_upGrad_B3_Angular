using System;
using System.Collections.Generic;

namespace ConsoleApp22
{
    class Program
    {
        static void Main()
        {
            List<string> tasks = new List<string>();

            while (true)
            {
                Console.WriteLine("\nTo-Do List Manager");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Remove Task");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TaskName.AddTask(tasks);
                        break;

                    case "2":
                        TaskName.ViewTasks(tasks);
                        break;

                    case "3":
                        TaskName.RemoveTask(tasks);
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}