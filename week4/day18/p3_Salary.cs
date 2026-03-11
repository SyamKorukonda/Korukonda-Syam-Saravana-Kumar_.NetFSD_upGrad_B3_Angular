namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "";
            Double salary = 0;
            int experience = 0;
            Double bonous = 0;
            Double finalSalary = 0;

            Console.WriteLine("enter name :");
            name = Console.ReadLine();
            Console.WriteLine("enter salary :");
            salary = Double.Parse(Console.ReadLine());
            Console.WriteLine("enter Experience :");
            experience = int.Parse(Console.ReadLine());

            /*
             * if (experience < 2)
            {
                bonous = salary * (0.05);
                finalSalary = salary + bonous;
            }
            else if (experience < 5 && experience > 2)
            {
                bonous = salary * (0.10);
                finalSalary = salary + bonous;
            }
            else
            {
                bonous = salary * (0.15);
                finalSalary = salary + bonous;

            }
            */
            // using ternary operators
            bonous = (experience < 2) ? salary * 0.05 :
                    (experience < 5) ? salary * 0.10 :
                    salary * 0.15;
            finalSalary = salary + bonous;

            Console.WriteLine($"Employee : {name}");
            Console.WriteLine($"Bonous : {bonous}");
            Console.WriteLine($"Final Salary : {finalSalary}");
            
            Console.ReadLine();

        }
    }
}
