namespace ConsoleApp21
{
    /*
     1.    Read array of numbers from the user 
2.    Send that array to a method :   GetEvenOddCounts()
3.    int[]   GetEvenOddTotals(int[]    ar)
4.    Finally display the result in main method
     */
    internal class Program
    {
        static int[] GetEvenOddTotals(int[] ar1) {
            int evenCount = 0;
            int oddCount = 0;
            //int[] totalCount = new int[2];
            foreach (int i in ar1) { 
                if(i%2==0)
                    evenCount++;
                else
                    oddCount++;

            }
            return new int[] { evenCount, oddCount };
        }
        static void Main(string[] args)
        {

            Console.Write("enter arry size n:");
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            Console.WriteLine("enter array elements:");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine();
            int[] result = GetEvenOddTotals(arr);
            Console.WriteLine("Even Count: " + result[0]);
            Console.WriteLine("Odd Count: " + result[1]);



            Console.ReadLine(); 
        }
    }
}
