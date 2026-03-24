using Microsoft.Data.SqlClient;

namespace ConsoleApp22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Server=SYAM\\SQLEXPRESS; Database=Employee; Integrated Security=true; TrustServerCertificate=True";

            SqlConnection con = new SqlConnection(connStr);

            string query = "SELECT * FROM Employees";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            Console.WriteLine("Connected to sql server...");

            // ExecuteReader() retrns SqlDataReader object

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["EmpId"]}, {reader["Name"]}, {reader["Department"]}, {reader["Salary"]}");

            }
            con.Close();
                

            Console.ReadLine();
        }
    }
}