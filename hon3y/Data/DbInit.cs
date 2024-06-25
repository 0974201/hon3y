using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;

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
                    Afspraakreden TEXT,
                    Datum
                );

                CREATE TABLE IF NOT EXISTS Inzendingen (
                    InzendingId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Voornaam TEXT,
                    Achternaam TEXT,
                    Email TEXT,
                    Bestand BLOB
                );";
                createTable.ExecuteNonQuery();
                PopulateDatabase();
            }
        }

        public void PopulateDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var checkTableContent = connection.CreateCommand();
                checkTableContent.CommandText = @"SELECT COUNT(*) FROM Login";

                var result = (long)checkTableContent.ExecuteScalar();

                Console.WriteLine(result);

                if (result == 0)
                {

                    //voegt gegevens toe aan de login tabel
                    var populateDatabase = connection.CreateCommand();
                    populateDatabase.CommandText = @"
                    INSERT INTO Login (Email, Password)
                    VALUES
                    ('admin@rotterdam.nl', 'admin'),
                    ('000000@rotterdam.nl', '12345678'),
                    ('111111@rotterdam.nl', 'qwerty'),
                    ('123456@rotterdam.nl', 'password'),
                    ('222222@rotterdam.nl', 'hunter2'),
                    ('420420@rotterdam.nl', 'ditiseenwachtwoord'),
                    ('808080@rotterdam.nl', 'Password1'),
                    ('133711@rotterdam.nl', 'azertyuiop'),
                    ('101010@rotterdam.nl', 'thisisapassword'),
                    ('geenadmin@rotterdam.nl', 'geenadmin');";

                    populateDatabase.ExecuteNonQuery();
                }
            }
        }
        public void CheckDBIntegrity()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var checkTable = connection.CreateCommand();
                checkTable.CommandText = @"PRAGMA integrity_check";
                
                var res = checkTable.ExecuteScalar().ToString();

                if (res != "ok")
                {
                    CreateTables();
                }
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
                    Log TEXT
                );";
                createTable.ExecuteNonQuery();
            }
        }
    }
}