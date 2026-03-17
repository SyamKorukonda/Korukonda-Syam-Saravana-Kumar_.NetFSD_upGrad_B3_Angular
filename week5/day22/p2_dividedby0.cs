namespace ConsoleApp8
{
    class Calculator
    {
        public void Divide(int numerator, int denominator)
        {
            try
            {
                int result = numerator / denominator;
                Console.WriteLine("Result = " + result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Cannot divide by zero");
            }
            finally
            {
                Console.WriteLine("Operation completed safely");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Calculator c = new Calculator();

            Console.Write("Enter Numerator: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Enter Denominator: ");
            int d = int.Parse(Console.ReadLine());

            c.Divide(n, d);

            Console.WriteLine("Program continues...");

            Console.ReadLine(); 
        }
    }

}
