using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql; //google ado net. eventueel periodiek kijken of db nog correct werkt, anders code schrijven dat db zichzelf ""repareert""
//overview project, met moscow (wat is er af/niet af), uitleg over de honeypot, hoe het evt uitgebreid kan worden, adviseren hoe verdergaan met product dat niet af is
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using hon3y.Models;
using Microsoft.Data.Sqlite;

namespace hon3y.Pages
{
    public class Afspraken : PageModel
    {

        private readonly ILogger<Afspraken> _logger;

        private readonly IDbConnection _connection;

        public Afspraken(ILogger<Afspraken> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        [BindProperty]
        public Afspraak Afspraak { get; set; } = new Afspraak();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Privacy");
            }

            try
            {
                using (var connection = (SqliteConnection)_connection)
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO Afspraken (Voornaam, Achternaam, Email, Telefoonnummer, Afspraakreden, Datum) VALUES (@Voornaam, @Achternaam, @Emailadres, @Telefoonnummer, @Afspraakreden, @Datum)";

                    command.Parameters.Add(new SqliteParameter("Voornaam", Afspraak.Voornaam ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Achternaam", Afspraak.Achternaam ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Emailadres", Afspraak.Email ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Telefoonnummer", Afspraak.Telefoonnummer));
                    command.Parameters.Add(new SqliteParameter("Afspraakreden", Afspraak.AfspraakReden ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Datum", Afspraak.Datum));

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: ");
            }

            // onderstaande is voor de logfile

            var voornaam = Request.Form["voornaam"];
            var achternaam = Request.Form["achternaam"];
            var emailadres = Request.Form["email"];
            var telefoonnummer = Request.Form["telefoonnummer"];
            var reden = Request.Form["afspraakreden"];
            var datum = Request.Form["datum"];

            _logger.LogInformation("Test");

            _logger.LogInformation(voornaam);
            _logger.LogInformation(achternaam);
            _logger.LogInformation(emailadres);
            _logger.LogInformation(telefoonnummer);
            _logger.LogInformation(reden);
            _logger.LogInformation(datum);

            return RedirectToPage("Succes");
        }
    }
}
