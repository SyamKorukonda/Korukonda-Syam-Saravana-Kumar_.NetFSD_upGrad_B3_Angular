namespace ConsoleApp10
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Enter a :  ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Enter b:  ");
            int b = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Operator [ +  -  *  / ]:  ");
            char op = char.Parse(Console.ReadLine());

            int c = 0;

            // Switch Case
            switch (op)
            {
                case '+':
                    c = a + b;
                    break;

                case '-':
                    c = a - b;
                    break;

                case '*':
                    c = a * b;
                    break;

                case '/':
                    if (b == 0)
                    {
                        Console.WriteLine("Error : Division by 0 is not allowed");
                        return;
                    }
                    else
                    {
                        c = a / b;
                    }
                    break;

                        default:
                    Console.WriteLine("Invalid Operation");
                    Console.ReadLine();
                    return;
            }


            Console.WriteLine("Final Result : " + c);

            Console.ReadLine();
        }
    }
}
