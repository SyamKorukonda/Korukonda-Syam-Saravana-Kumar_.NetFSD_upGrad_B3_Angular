using Dapper;
using System.Data.SqlClient;
using WebApplication10.Models;

namespace WebApplication10.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly string _connStr;

        public ContactRepository(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }

        public IEnumerable<ContactInfo> GetAllContacts()
        {
            var sqlQuery = @"SELECT c.*,cp.CompanyName,d.DepartmentName 
                            FROM ContactInfo c
                            JOIN Company cp ON c.CompanyId=cp.CompanyId
                            LEFT JOIN Department d ON d.DepartmentId=c.DepartmentId";
            var conn = GetConnection();
            return conn.Query<ContactInfo>(sqlQuery);
        }
        public ContactInfo GetContactById(int id)
        {
            var sqlQ = "SELECT * FROM ContactInfo WHERE ContactId = @Id";
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<ContactInfo>(sqlQ, new { Id = id });
        }

        public void AddContact(ContactInfo contact)
        {
            string sqlQuery = @"INSERT INTO ContactInfo 
                (FirstName, LastName, EmailId, MobileNo, Designation, CompanyId, DepartmentId)
                VALUES (@FirstName, @LastName, @EmailId, @MobileNo, @Designation, @CompanyId, @DepartmentId)";

            var db = GetConnection();
            db.Execute(sqlQuery, contact);
        }
        public void UpdateContact(ContactInfo contact)
        {
            string sqlQuery = @"UPDATE ContactInfo SET 
                FirstName=@FirstName,
                LastName=@LastName,
                EmailId=@EmailId,
                MobileNo=@MobileNo,
                Designation=@Designation,
                CompanyId=@CompanyId,
                DepartmentId=@DepartmentId
                WHERE ContactId=@ContactId";

            var db = GetConnection();
            db.Execute(sqlQuery, contact);
        }
        public void DeleteContact(int id)
        {
            string sqlQuery = "DELETE FROM ContactInfo WHERE ContactId = @Id";

            using var db = GetConnection();
            db.Execute(sqlQuery, new { Id = id });
        }

        // Dropdowns
        public IEnumerable<Company> GetCompanies()
        {
            string sqlQuery = "SELECT * FROM Company";

            var db = GetConnection();
            return db.Query<Company>(sqlQuery);
        }
        public IEnumerable<Department> GetDepartments()
        {
            string sqlQuery = "SELECT * FROM Department";

            var db = GetConnection();
            return db.Query<Department>(sqlQuery);
        }
    }
}
