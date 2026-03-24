using Microsoft.Data.SqlClient;

namespace ConsoleApp22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Server=SYAM\\SQLEXPRESS; Database=Employee; Integrated Security=true; TrustServerCertificate=True";
            SqlConnection con = new SqlConnection(connStr);

            Console.WriteLine("Enter EmpId to delete : ");
            int eno = int.Parse(Console.ReadLine());

            string cmdText = $"DELETE FROM Employees WHERE EmpId={eno}";
            SqlCommand cmd = new SqlCommand(cmdText, con);

            con.Open();

            // ExecuteNonQuery()  Returns int
            // no. of rows affected
            int n = cmd.ExecuteNonQuery();  // for DML Commands

            Console.WriteLine("Connected to SQL Server");
            Console.WriteLine("No. of Rows affected : " + n);

            con.Close();

            Console.ReadLine();
        }
    }
}