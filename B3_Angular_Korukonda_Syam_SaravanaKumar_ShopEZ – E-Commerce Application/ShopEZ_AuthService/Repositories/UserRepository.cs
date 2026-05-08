using System.Data;
using Dapper;
using ShopEZ_AuthService.Data;
using ShopEZ_AuthService.Models;

namespace ShopEZ_AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        // IDbConnectionFactory creates SQL connection for Dapper

        private readonly IDbConnectionFactory _connectionFactory;

        // Constructor — Dependency Injection
        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Find user by email — used in Login
        public async Task<User?> GetByEmailAsync(string email)
        {
            // Open connection — auto closed after method finishes (using)

            using IDbConnection db = _connectionFactory.CreateConnection();

            // Select all fields needed for login and JWT token generation

            const string sql = @"
                SELECT UserId, UserName, EmailAddress, PasswordHash, Role
                FROM   Users
                WHERE  EmailAddress = @Email";

            // Returns one User or null if not found
            // Trim and ToLower for case-insensitive email matching

            return await db.QuerySingleOrDefaultAsync<User>(sql, new { Email = email.Trim().ToLower() });
        }

        // Find user by ID
        public async Task<User?> GetByIdAsync(int userId)
        {
            // Open connection — auto closed after method finishes (using)

            using IDbConnection db = _connectionFactory.CreateConnection();

            // Select user by primary key

            const string sql = @"
                SELECT UserId, UserName, EmailAddress, PasswordHash, Role
                FROM   Users
                WHERE  UserId = @UserId";

            // Returns one User or null if not found

            return await db.QuerySingleOrDefaultAsync<User>(sql, new { UserId = userId });
        }

        // Get all users — used in UsersController (Admin only)
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            // Open connection — auto closed after method finishes (using)

            using IDbConnection db = _connectionFactory.CreateConnection();

            // PasswordHash excluded from SELECT — never expose it to the client

            const string sql = @"
                SELECT UserId, UserName, EmailAddress, Role
                FROM   Users
                ORDER BY UserName ASC";

            // QueryAsync returns multiple rows as a list

            return await db.QueryAsync<User>(sql);
        }

        // Check if email already exists — used in Register to prevent duplicates
        public async Task<bool> EmailExistsAsync(string email)
        {
            // Open connection — auto closed after method finishes (using)

            using IDbConnection db = _connectionFactory.CreateConnection();

            // COUNT(1) returns 1 if found, 0 if not found

            const string sql = @"
                SELECT COUNT(1)
                FROM   Users
                WHERE  EmailAddress = @Email";

            // ExecuteScalarAsync returns a single value (the count)
            // Trim and ToLower for case-insensitive email matching

            var count = await db.ExecuteScalarAsync<int>(sql, new { Email = email.Trim().ToLower() });
            return count > 0;
        }

        // Save new user to database — used in Register
        public async Task<int> CreateAsync(User user)
        {
            // Open connection — auto closed after method finishes (using)

            using IDbConnection db = _connectionFactory.CreateConnection();

            // Insert user and return the auto-generated UserId using SCOPE_IDENTITY()

            const string sql = @"
                INSERT INTO Users (UserName, EmailAddress, PasswordHash, Role)
                VALUES            (@UserName, @EmailAddress, @PasswordHash, @Role);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            // Pass parameters safely — prevents SQL injection
            // Returns the newly generated UserId

            int newId = await db.ExecuteScalarAsync<int>(sql, new
            {
                UserName = user.UserName.Trim(),
                EmailAddress = user.EmailAddress.Trim().ToLower(),
                PasswordHash = user.PasswordHash,  // already BCrypt hashed
                Role = user.Role
            });

            return newId;
        }
    }
}