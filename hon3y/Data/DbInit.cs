using System;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;

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

        public void CreateDatabase(string databaseName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string createDatabaseQuery = $"IF DB_ID('{databaseName}') IS NULL CREATE DATABASE {databaseName}";
                SqlCommand command = new SqlCommand(createDatabaseQuery, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void CreateTables(string testDB)
        {
            string databaseConnectionString = _configuration.GetConnectionString("DatabaseConnection");

            using (SqlConnection connection = new SqlConnection(databaseConnectionString))
            {
                string createTableQuery = @"
                IF OBJECT_ID('dbo.Test', 'U') IS NULL
                CREATE TABLE Test (
                    Id INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(50) NOT NULL
                )";
                SqlCommand command = new SqlCommand(createTableQuery, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}