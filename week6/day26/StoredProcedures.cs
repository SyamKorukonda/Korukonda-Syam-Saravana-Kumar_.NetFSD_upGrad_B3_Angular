using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace ConsoleApp41
{
    class Program
    {
        static void Main()
        {
            // Note: Set this property to appsettings.json file
            // "Copy to Output Directory" → Copy if newer 

            // Build configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Read connection string
            string connStr = config.GetConnectionString("DefaultConnection");


            SqlConnection conn = new SqlConnection(connStr);

            string cmdText = "GetEmployeeCountByDept";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Specify to execute stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@DeptNo";
            p1.SqlDbType = SqlDbType.Int;
            p1.Direction = ParameterDirection.Input;
            p1.Value = 10;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@EmpCount";
            p2.SqlDbType = SqlDbType.Int;
            p2.Direction = ParameterDirection.Output;


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);


            cmd.ExecuteNonQuery();

            Console.WriteLine("Employee Count : " + p2.Value);
            conn.Close();


            Console.ReadLine(); // Waiting the command prompt 
        }
    }
}
