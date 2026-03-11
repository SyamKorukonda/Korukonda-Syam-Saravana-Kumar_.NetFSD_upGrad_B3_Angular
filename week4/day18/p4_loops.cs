namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            Console.WriteLine("enter  number  n :");
            n=int.Parse(Console.ReadLine());
            int ecount = 0;
            int ocount = 0;
            int sum = 0;

            for(int i = 1; i <= n; i++)
            {
                sum += i;
                if (i % 2 == 0)
                    ecount++;
                else
                    ocount++;
            }

            Console.WriteLine($"Even count : {ecount}");
            Console.WriteLine($"Odd count : {ocount}");
            Console.WriteLine($"Sum : {sum}");

            Console.ReadLine(); 
        }
    }
}
