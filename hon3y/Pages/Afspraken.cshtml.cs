using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql; //google ado net. eventueel periodiek kijken of db nog correct werkt, anders code schrijven dat db zichzelf ""repareert""
//overview project, met moscow (wat is er af/niet af), uitleg over de honeypot, hoe het evt uitgebreid kan worden, adviseren hoe verdergaan met product dat niet af is
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using hon3y.Models;
using Microsoft.Data.Sqlite;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace hon3y.Pages
{
    public class Afspraken : PageModel
    {

        private readonly ILogger<Afspraken> _logger;

        private readonly IDbConnection _connection;

        public Afspraken(ILogger<Afspraken> logger, IDbConnection connection)
        {
            _logger = logger; //roept de logger voor deze file aan
            _connection = connection; //connectie met de database
        }

        [BindProperty]
        public Afspraak Afspraak { get; set; } = new Afspraak();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                using (var connection = (SqliteConnection)_connection)
                {
                    connection.Open();

                    var voornaam = Request.Form["voornaam"];
                    var achternaam = Request.Form["achternaam"];
                    var emailadres = Request.Form["email"];
                    var telefoonnummer = Request.Form["telefoonnummer"];
                    var afspraakreden = Request.Form["afspraakreden"];
                    var datum = Request.Form["datum"];

                    //geen prepared statement gebruiken voor extra veiligheid
                    var statement = $@"INSERT INTO Afspraken (Voornaam, Achternaam, Email, Telefoonnummer, Afspraakreden, Datum) VALUES ('{voornaam}', '{achternaam}', '{emailadres}', '{telefoonnummer}', '{afspraakreden}', '{datum}')";

                    var command = new SqliteCommand(statement, connection);
                    await command.ExecuteNonQueryAsync();

                    _logger.LogInformation(statement);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: An error occurred");
            }

            // onderstaande is voor de logfile

            var voornaam_log = Request.Form["voornaam"];
            var achternaam_log = Request.Form["achternaam"];
            var emailadres_log = Request.Form["email"];
            var telefoonnummer_log = Request.Form["telefoonnummer"];
            var reden_log = Request.Form["afspraakreden"];
            var datum_log = Request.Form["datum"];

            _logger.LogInformation("Test");

            _logger.LogInformation(voornaam_log);
            _logger.LogInformation(achternaam_log);
            _logger.LogInformation(emailadres_log);
            _logger.LogInformation(telefoonnummer_log);
            _logger.LogInformation(reden_log);
            _logger.LogInformation(datum_log);

            return RedirectToPage("Succes");
        }
    }
}
