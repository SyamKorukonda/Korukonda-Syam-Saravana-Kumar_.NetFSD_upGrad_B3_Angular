using System.Collections.Generic;

namespace ConsoleApp8
{

    class Program
    {

        static void Main()
        {
            // Collection Initializer
            List<String> list = new List<String>();

            while (true)
            {

                Console.WriteLine("\nTo-Do List Manager");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Remove Task");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        Console.WriteLine("enter task:");
                        string task = Console.ReadLine();
                        if (string.IsNullOrEmpty(task))
                            Console.WriteLine(" task cannot be empty ");
                        else
                        {
                            list.Add(task);
                            Console.WriteLine(" task is added");
                        }
                        break;

                    case "2":

                        if (list.Count == 0)
                            Console.WriteLine(" no task available");
                        Console.WriteLine("View task");
                        for (int i = 0; i < list.Count; i++)
                            Console.WriteLine((i + 1) + "." + list[i]);
                        break;

                    case "3":
                        Console.Write(" enter task  number to remove :");
                        String inpt = Console.ReadLine();
                        if (int.TryParse(inpt, out int val) && val >= 1 && val <= list.Count)
                        {
                            String remove = list[val - 1];
                            list.RemoveAt(val - 1);
                            Console.WriteLine("removed : " + remove);
                        }
                        else
                            Console.WriteLine("Invalid index");
                        break;

                    case "4":
                        Console.WriteLine("Exit");
                        return;

                    default:
                        Console.WriteLine(" invalid option");
                        break;
                }

                Console.ReadLine();
            }
        }

    }
}
