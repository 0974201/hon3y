using System;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Microsoft.Data.Sqlite;

namespace hon3y.Data
{
    public class DbInit
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbInit(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public void CreateDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
            }
        }

        public void CreateTables()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS TestDB (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );";
                createTableCmd.ExecuteNonQuery();
            }
        }
    }
}