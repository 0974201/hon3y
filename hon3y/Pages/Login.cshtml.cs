using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Data;

namespace hon3y.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        private readonly IDbConnection _connection;

        public LoginModel(ILogger<LoginModel> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            try
            {
                using (var connection = (SqliteConnection)_connection)
                {
                    connection.Open();

                    var statement = $"SELECT * FROM Login WHERE Email = '{email}' AND Password = '{password}'";

                    Console.WriteLine(statement); 
                    _logger.LogInformation(statement);

                    var command = new SqliteCommand(statement, connection);

                    using (var reader = command.ExecuteReader()) {

                        if (reader.Read())
                        {
                            var getEmail = reader.GetOrdinal("Email");

                            if (!reader.IsDBNull(getEmail))
                            {
                                var getRow = reader.GetString(getEmail);
                                _logger.LogInformation($"Login poging geslaagd met email: '{email}' en wachtwoord '{password}'");
                                Console.WriteLine(getEmail.ToString(), getRow);

                                return RedirectToPage("LoginSuccess");
                            }
                            else
                            {
                                _logger.LogWarning("Kolom 'Email' bevat geen gegevens.");
                            }

                            _logger.LogInformation(reader.GetOrdinal("Email").ToString());
                            
                            Console.WriteLine(reader.GetOrdinal("Email").ToString());
                        }
                        else {
                            _logger.LogInformation($"Login poging mislukt met email: '{email}' en wachtwoord '{password}'");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: An error occurred");
                return Page();
            }

            return RedirectToPage("Succes");
        }
    }
}
