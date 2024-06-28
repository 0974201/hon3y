using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;

namespace hon3y.Data
{
    public class DbInit
    {
        private readonly IConfiguration _configuration; //verwijst naar de configuratie in startup.cs, nodig voor de connectie
        private readonly string _connectionString; //connectie met de db voor de website
        private readonly string _connectionString2; //connectie met db voor de logs

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
                connection.Open(); //maakt de database aan als het niet bestaat
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

                //controleert eerst of er entries zijn in de login tabel, als die er niet zijn dan worden er gegevens toegevoegd
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

                //controleert of de tabellen aanwezig zijn, als ze er niet zijn worden ze opnieuw aangemaakt.

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
                connection.Open(); //maakt de database aan als het niet bestaat
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