using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace hon3y.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        private readonly IDbConnection _connection;

        public LoginModel(ILogger<LoginModel> logger, IDbConnection connection)
        {
            _logger = logger; //roept de logger voor deze file aan
            _connection = connection; //roept de logger voor deze file aan
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

                    //statement om gebruiker op te halen
                    var statement = $"SELECT * FROM Login WHERE Email = '{email}' AND Password = '{password}'";

                    Console.WriteLine(statement); 
                    _logger.LogInformation(statement);

                    var command = new SqliteCommand(statement, connection);

                    using (var reader = command.ExecuteReader()) {

                        if (reader.HasRows)
                        {
                            //als gebruiker bestaat dan wordt er verwezen naar de login succes pagina en de inlogpoging gelogd
                            _logger.LogInformation($"Login poging geslaagd met email: '{email}' en wachtwoord '{password}'");
                            return RedirectToPage("LoginSucces");
                         }
                        else
                        {
                            //mislukte inlogpoging loggen
                            _logger.LogInformation($"Login poging mislukt met email: '{email}' en wachtwoord '{password}'");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error: An error occurred");
                return Page();
            }

            return RedirectToPage("Succes");
        }
    }
}