namespace ConsoleApp9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "";
            int marks = 0;
            Console.Write("enter name:");
            name = Console.ReadLine();
            Console.Write("enter marks:");
            marks = int.Parse(Console.ReadLine());

            if (marks >= 90)
            {
                Console.WriteLine($"Student : {name} ");
                Console.WriteLine(" Grade : A");
            }
            else if (marks >=80 && marks < 90)
            {
                Console.WriteLine($"Student : {name} ");
                Console.WriteLine(" Grade : B");
            }
            else if (marks >= 70 && marks < 80)
            {
                Console.WriteLine($"Student : {name} ");
                Console.WriteLine(" Grade : C");
            }
            else if(marks >= 50 && marks < 70){
                Console.WriteLine($"Student : {name} ");
                Console.WriteLine(" Grade : D");
            }
            else {
                Console.WriteLine($"Student : {name} ");
                Console.WriteLine("Fail");
            }

            Console.ReadLine ();
        }
    }
}
