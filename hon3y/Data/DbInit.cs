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
        private readonly string _connectionString2; //electric boogaloo

        public DbInit(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connectionString2 = _configuration.GetConnectionString("NotDefaultConnection");
        }

        public void CreateDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
            }
        }

        /* public void CheckDatabase()
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var checkTable = connection.CreateCommand();
                checkTable.CommandText = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Login'";
               
                var tableExists = (long)checkTable.ExecuteScalar() > 0;

                if (!tableExists)
                {
                    CreateTables();
                }
            }
        } */

        public void CreateTables()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //maakt tabellen aan als ze niet bestaan

                var createTable = connection.CreateCommand();
                createTable.CommandText = @"
                CREATE TABLE IF NOT EXISTS Login (
                    LoginId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Email TEXT,
                    Password TEXT
                );

                CREATE TABLE IF NOT EXISTS Afspraken (
                    AfspraakId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Voornaam TEXT,
                    Achternaam TEXT,
                    Email TEXT,
                    Telefoonnummer INTEGER,
                    AfspraakReden TEXT,
                    Datum
                );

                CREATE TABLE IF NOT EXISTS Inzendingen (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Voornaam TEXT,
                    Achternaam TEXT,
                    Email TEXT,
                    Bestand BLOB
                );";
                createTable.ExecuteNonQuery();
            }
        }

        public void CreateLogsDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString2))
            {
                connection.Open();
            }
        }

        public void CreateLogDBTable()
        {
            using (var connection = new SqliteConnection(_connectionString2))
            {
                connection.Open();

                var createTable = connection.CreateCommand();
                createTable.CommandText = @"
                CREATE TABLE IF NOT EXISTS Logs (
                    LogId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Datum TEXT,
                    Email TEXT,
                    Password TEXT
                    Voornaam TEXT,
                    Achternaam TEXT,
                    Telefoonnummer INTEGER,
                    AfspraakReden TEXT,
                    Bestand BLOB
                );";
                createTable.ExecuteNonQuery();
            }
        }
    }
}