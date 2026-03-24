using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace ConsoleApp41
{
    class Program
    {
        static void Main()
        {
            var confg = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();
            string conStr = confg.GetConnectionString("DefaultConnection");

            while (true)
            {
                Console.WriteLine("\n 1.Insert  2.View  3.Update  4.Delete  5.Exit");
                Console.Write("Choose : ");
                int c = int.Parse(Console.ReadLine());

                using SqlConnection con = new SqlConnection(conStr);
                con.Open();

                switch (c)
                {
                    case 1:
                        Console.Write("Enter name:");
                        String name = Console.ReadLine();
                        Console.Write("Category: ");
                        string category = Console.ReadLine();

                        Console.Write("Price: ");
                        decimal price = Convert.ToDecimal(Console.ReadLine());

                        SqlCommand insert = new SqlCommand("sp_InsertProduct", con);
                        insert.CommandType = CommandType.StoredProcedure;
                        insert.Parameters.AddWithValue("@ProductName", name);
                        insert.Parameters.AddWithValue("@Category", category);
                        insert.Parameters.AddWithValue("@Price", price);

                        insert.ExecuteNonQuery();
                        Console.WriteLine("Values inserted");
                        break;

                    case 2:
                        SqlCommand select = new SqlCommand("sp_GetAllProducts", con);
                        select.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = select.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Console.WriteLine($"{dr["ProductId"]} {dr["ProductName"]} {dr["Category"]} {dr["Price"]}");
                            }
                        }
                        break;

                    case 3:
                        Console.Write("Enter Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("New Name: ");
                        string n = Console.ReadLine();

                        Console.Write("New Category: ");
                        string ct = Console.ReadLine();

                        Console.Write("New Price: ");
                        decimal p = Convert.ToDecimal(Console.ReadLine());

                        SqlCommand update = new SqlCommand("sp_UpdateProduct", con);
                        update.CommandType = CommandType.StoredProcedure;

                        update.Parameters.AddWithValue("@ProductId", id);
                        update.Parameters.AddWithValue("@ProductName", n);
                        update.Parameters.AddWithValue("@Category", ct);
                        update.Parameters.AddWithValue("@Price", p);

                        update.ExecuteNonQuery();
                        Console.WriteLine("Updated");
                        break;

                    case 4:
                        Console.Write("Enter Id: ");
                        int did = int.Parse(Console.ReadLine());

                        SqlCommand delete = new SqlCommand("sp_DeleteProduct", con);
                        delete.CommandType = CommandType.StoredProcedure;
                        delete.Parameters.AddWithValue("@ProductId", did);

                        delete.ExecuteNonQuery();
                        Console.WriteLine("Deleted ");
                        break;

                    case 5:
                        return;
                    default:
                        Console.WriteLine("invalid choice");
                        break;

                }

                Console.ReadLine(); // Waiting the command prompt 
            }
        }
    }
}
