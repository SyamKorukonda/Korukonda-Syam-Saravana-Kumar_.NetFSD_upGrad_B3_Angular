

/*
 Create a Method to Count Vowels in a String
Task:
Write a method that counts vowels in a string.
Method Name: CountVowels(string text)
Example
Input: Programming
Output: 3
 */


namespace ConsoleApp14
{
    internal class Program
    {

        static void  CountVowels(string text)
        {
            int count = 0;
            foreach(char c in text.ToLower())
            {
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
        static void Main(string[] args)
        {
            CountVowels("Programming");
            Console.ReadLine(); 
        }
    }
}
