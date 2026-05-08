using System.Data;
using Dapper;

namespace ShopEZ_AuthService.Data
{
    // Runs at startup to create the Users table if it does not already exist
  
    public class DatabaseInitializer
    {
        // Connection factory to create SQL connection for Dapper
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ILogger<DatabaseInitializer> _logger;

        // Constructor — Dependency Injection
        public DatabaseInitializer(IDbConnectionFactory connectionFactory, ILogger<DatabaseInitializer> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                // Open connection — auto closed after method finishes (using)
                using IDbConnection db = _connectionFactory.CreateConnection();

                const string createUsersTable = @"

--[ Check if Users table already exists before creating
                    -- sysobjects is a SQL Server system table that stores all database objects
                    -- xtype = 'U' means User Table ]

                    IF NOT EXISTS (
                        SELECT 1 FROM sysobjects
                        WHERE name = 'Users' AND xtype = 'U'
                    )
                    BEGIN
                        CREATE TABLE Users (
                            UserId       INT IDENTITY(1,1) PRIMARY KEY,
                            UserName     NVARCHAR(100)  NOT NULL,
                            EmailAddress NVARCHAR(256)  NOT NULL,
                            PasswordHash NVARCHAR(MAX)  NOT NULL,
                            Role         NVARCHAR(50)   NOT NULL DEFAULT 'Customer',

                            CONSTRAINT UQ_Users_Email UNIQUE (EmailAddress),
                            CONSTRAINT CK_Users_Role  CHECK  (Role IN ('Admin', 'Customer'))
                        );

    -- Index on EmailAddress for faster login queries (WHERE EmailAddress = @Email)

                        CREATE INDEX IX_Users_Email ON Users (EmailAddress);
                    END";

                // Execute the SQL to create the table
                db.Execute(createUsersTable);
                _logger.LogInformation("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database initialization failed.");
                throw;
            }
        }
    }
}
