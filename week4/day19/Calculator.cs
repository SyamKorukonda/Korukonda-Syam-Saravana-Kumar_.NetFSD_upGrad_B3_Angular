
/*
 A small retail shop wants a simple calculator application to perform addition and subtraction operations using reusable methods.
Requirements:
1. Create a class named Calculator.
2. Create methods Add(int a, int b) and Subtract(int a, int b).
3. Each method should return the result.
4. In Main(), create an object and call the methods.
5. Display the output.

 */

namespace ConsoleApp15
{

    class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }
        internal class Program
        {
            static void Main(string[] args)
            {
                Calculator c = new Calculator();
                Console.WriteLine($"Addition = {c.Add(10, 5)} , Suptracton = {c.Subtract(10, 5)}");
                Console.ReadLine();
            }
        }
    
}
