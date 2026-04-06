using Dapper;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WebApplication9.Models;

namespace WebApplication9.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connStr;
        public ProductRepository(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }

        /*
        public IEnumerable<Product> GetAll()
        {
            string sqlQuery = "SELECT * FROM Products";
            var conn = GetConnection();
            return conn.Query<Product>(sqlQuery);
        }
        */
        // Get All Products
        public IEnumerable<Product> GetAll()
        {
            string sqlQuery = "SELECT * FROM Products";

            using (var conn = GetConnection())
            {
                return conn.Query<Product>(sqlQuery);
            }
        }

        /*
        public Product GetById(int id)
        {
                string sqlQuery = "SELECT * FROM Products WHERE Id= @Id";
                var conn = GetConnection();
                return conn.QueryFirstOrDefault<Product>(sqlQuery);

        }
        */
        //Get Product By Id
        public Product GetById(int id)
        {
            string sqlQuery = "SELECT * FROM Products WHERE Id = @Id";

            using (var conn = GetConnection())
            {
                return conn.QueryFirstOrDefault<Product>(sqlQuery, new { Id = id }); //// new { Id = id }  Passes value safely to @Id
            }
        }
        /*
        public void Add(Product product)
        {
            string sqlQuery = @"INSERT INTO Products (Name, Price, Category) VALUES (@Name, @Price, @Category)";

            var conn = GetConnection();
            conn.Execute(sqlQuery, product);
        }
        */
        //  Add Product
        public void Add(Product product)
        {
            string sqlQuery = @"INSERT INTO Products (Name, Price, Category) 
                                VALUES (@Name, @Price, @Category)";

            using (var conn = GetConnection())
            {
                conn.Execute(sqlQuery, product);
            }
        }
        public void Update(Product product)
        {
            string sqlQuery = @"UPDATE Products  
                                    SET Name=@Name, Price=@Price,Category=@Category
                                     WHERE ID=@ID";
            using(var conn = GetConnection())
            {
                conn.Execute(sqlQuery, product);
            }
        }
        public void Delete(int id)
        {
            String sqlQuery = "DELETE FROM Products WHERE ID="+id;
            using(var  conn = GetConnection())
            {
                conn.Execute(sqlQuery);   // new { Id = id }  Passes value safely to @Id
            }
        }


    }
}
