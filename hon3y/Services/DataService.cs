
using Microsoft.Data.Sqlite;

namespace hon3y.Services
{
    public class DataService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _connectionString2; //electric boogaloo

        public DataService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connectionString2 = _configuration.GetConnectionString("NotDefaultConnection");
        }

        public void InsertDataAfspraken()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Afspraken (Voornaam, Achternaam, Email, Telefoonnummer, Afspraakreden, Datum) VALUES (?,?,?,?,?,?)";
                command.ExecuteNonQuery();
            }
        }

        public void InsertDataInzendingen()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Inzendingen (Voornaam, Achternaam, Email, Bestand) VALUES (?,?,?,?)";
                command.ExecuteNonQuery();
            }
        }

        public void GetLogin()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Login WHERE Email = ? ()";
                command.ExecuteNonQuery();
            }
        }
    }
}
