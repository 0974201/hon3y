using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Data;
using hon3y.Models;

namespace hon3y.Pages
{
    public class Inzendingen : PageModel
    {

        private readonly ILogger<Inzendingen> _logger;

        private readonly IDbConnection _connection;

        public Inzendingen(ILogger<Inzendingen> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        [BindProperty]
        public Inzending Inzending { get; set; } = new Inzending();
       
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var voornaam = Request.Form["voornaam"];
            var achternaam = Request.Form["achternaam"];
            var emailadres = Request.Form["email"];
            var bestand = Request.Form.Files["uploadedFile"];

            if (!ModelState.IsValid)
            {
                return RedirectToPage("Privacy");
            }

            byte[] data = null;

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                
                if (file.Length > 0)
                { 
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        data = stream.ToArray();
                    }
                } 
            }

            try
            {
                using (var connection = (SqliteConnection) _connection)
                {
                    connection.Open();

                    var bestand2 = data != null ? BitConverter.ToString(data).Replace("-", "") : null; ;

                    Console.WriteLine(voornaam);
                    Console.WriteLine(achternaam);
                    Console.WriteLine(emailadres);
                    Console.WriteLine(bestand);
                    Console.WriteLine(bestand2);

                    var statement = $@"INSERT INTO Inzendingen (Voornaam, Achternaam, Email, Bestand) VALUES ('{voornaam}', '{achternaam}', '{emailadres}', '{bestand}')";

                    Console.WriteLine(statement);

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
            var uploadedFile_log = Request.Form["uploadedFile"];

            _logger.LogInformation("Test");

            _logger.LogInformation(voornaam_log);
            _logger.LogInformation(achternaam_log);
            _logger.LogInformation(emailadres_log);
            _logger.LogInformation(uploadedFile_log);

            return RedirectToPage("Succes");
        }
    }
}
