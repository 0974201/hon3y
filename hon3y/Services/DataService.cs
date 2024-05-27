
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

        public void InsertData()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO {Table} (val1, val2, val3, val4, val5) VALUES (?,?,?,?,?)";
                command.ExecuteNonQuery();
            }
        }
    }
}
