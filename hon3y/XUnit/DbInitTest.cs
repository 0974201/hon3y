using Microsoft.Data.Sqlite;
using Xunit;

namespace hon3y.XUnit
{
    public class DbInitTest
    {
        private string _connectionString = "Data Source=:memory:";
        
        [Fact]
        public void PopulateDatabase_ShouldInsertData_WhenTableIsEmpty()
        {
            var dbInit = new DbInit(_connectionString);
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                
                //Maak login tabel aan
                var createTableCommand = connection.CreateCommand();
               
                createTableCommand.CommandText = @"
                CREATE TABLE Login (Email TEXT NOT NULL,Password TEXT NOT NULL);";
                
                createTableCommand.ExecuteNonQuery();
            }

            // insert data
            dbInit.PopulateDatabase();

            //testen of data in tabel zit
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var countCommand = connection.CreateCommand();
                countCommand.CommandText = "SELECT COUNT(*) FROM Login;";
                var count = (long)countCommand.ExecuteScalar();

                Assert.Equal(10, count);
            }
        }

        [Fact]
        public void PopulateDatabase_ShouldNotInsertData_WhenTableIsNotEmpty()
        {
            var dbInit = new DbInit(_connectionString);
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                
                var createTableCommand = connection.CreateCommand();
                
                createTableCommand.CommandText = @"
                CREATE TABLE Login (Email TEXT NOT NULL,Password TEXT NOT NULL);";
                
                createTableCommand.ExecuteNonQuery();
                
                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO Login (Email, Password) VALUES ('test@rotterdam.nl', 'test');";
                insertCommand.ExecuteNonQuery();
            }
            
            dbInit.PopulateDatabase();
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var countCommand = connection.CreateCommand();
                countCommand.CommandText = "SELECT COUNT(*) FROM Login;";
                var count = (long)countCommand.ExecuteScalar();
                
                // Should still be 1 because the table was not empty
                Assert.Equal(1, count);
            }
        }
        
        public class DbInit
        {
            private readonly string _connectionString;
            
            public DbInit(string connectionString)
            {
                _connectionString = connectionString;
            }
            
            public void PopulateDatabase()
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    // controleert of er al data in de login tabel zit
                    var checkDataCommand = connection.CreateCommand();
                    checkDataCommand.CommandText = "SELECT COUNT(*) FROM Login;";

                    var count = (long)checkDataCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        //voeg data toe als het er niet is
                        var populateDatabaseCommand = connection.CreateCommand();
                        populateDatabaseCommand.CommandText = @"
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

                        populateDatabaseCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}