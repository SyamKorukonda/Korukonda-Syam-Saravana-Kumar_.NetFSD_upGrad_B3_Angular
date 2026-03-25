using System;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp41
{
    class Program
    {
        static string conStr;

        static void Main()
        {
            //Build Configuration 

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            conStr = config.GetConnectionString("DefaultConnection");
            DataTable dt = GetData();

            while (true)
            {
                Console.WriteLine("\n 1.Insert ,2. GetAllData, 3.Update ,4.Delete 5.Exit");
                Console.Write("Enter Option : ");
                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        InsertData(dt);
                        break;
                    case 2:
                        Display(dt  );
                        break;
                    case 3:
                        UpdateData(dt);
                        break;
                    case 4:
                        DeleteData(dt);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("enter valid option ");
                        break;

                }





                //Console.ReadLine();
            }
        }

        //GetData
        public static DataTable GetData()
        { 
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", conStr);// adapter
            //command Builder for auto insert,delete,update
            SqlCommandBuilder cmd=new SqlCommandBuilder(adapter);
            DataTable dt = new DataTable();// DataTable (Disconnected)
            adapter.Fill(dt); //method loads data from the database into a DataTable or DataSet
                              //and automatically manages the connection.
            return dt;  
        }

        public static void InsertData(DataTable dt)
        {
            Console.WriteLine("\n Inserting Data ");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", conStr);
            SqlCommandBuilder cmd=new SqlCommandBuilder(adapter);   
            
            DataRow row = dt.NewRow();

            Console.Write("Enter Product Name: ");
            row["ProductName"] = Console.ReadLine();

            Console.Write("Enter Product Category: ");
            row["Category"] = Console.ReadLine();

            Console.Write("Enter Price: ");
            row["Price"] = Convert.ToDecimal(Console.ReadLine());

            dt.Rows.Add(row);
            adapter.Update(dt);

            Console.WriteLine("Inserted Successfully");
        }


        public static void UpdateData(DataTable dt)
        {
            Console.WriteLine("\n Update Price  by ID ");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", conStr);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            Console.Write("Enter ProductId: ");
            int id = int.Parse(Console.ReadLine());

            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["ProductId"] == id)
                {
                    Console.Write("Enter New Price: ");
                    row["Price"] = Convert.ToDecimal(Console.ReadLine());
                    break;
                }
            }

            adapter.Update(dt);
            Console.WriteLine("Updated Successfully");
            //  Refresh to get IDENTITY value
            dt.Clear();
            adapter.Fill(dt);


        }

        public static void DeleteData(DataTable dt)
        {
            Console.WriteLine("\n Delete Data");
           
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", conStr);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            Console.Write("Enter ProductId: ");
            int id = int.Parse(Console.ReadLine());

            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["ProductId"] == id)
                {
                    row.Delete();
                    break;
                }
            }

            adapter.Update(dt);
            Console.WriteLine("Deleted Successfully");
            

        }
        
        public static void Display(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    Console.WriteLine($"Id : {row["ProductId"]}, Name: {row["ProductName"]}, Price: {row["Price"]}");
                }
            }
        }



    }
}