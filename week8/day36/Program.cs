using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

namespace ConsoleApp27
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Category { get; set; }    
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var conStr = "Server=SYAM\\SQLEXPRESS; Database=ProductsDb;Integrated Security=True; Trusted_Connection=True;TrustServerCertificate=True";
            using(var con=new SqlConnection(conStr))
            {
                var products = new List<Product>
                {
                    new Product {Name="Mango",Price=90,Category="Fruits" },
                    new Product{Name="Apple",Price=40,Category="Fruits"}
                };

                var sql = "INSERT INTO Products(Name,Price,Category) VALUES(@name,@Price,@Category)";

                con.Execute(sql, products);
            };

            Console.WriteLine("New Products are inserted in the database Sussfully");


            Console.ReadLine();
        }
    }
}
