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

        /*public void OnPost()
        {
            
        }*/

        public async Task<IActionResult> OnPostAsync()
        {
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
                    
                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO Inzendingen (Voornaam, Achternaam, Email, Bestand) VALUES (@Voornaam, @Achternaam, @Emailadres, @Bestand)";

                    command.Parameters.Add(new SqliteParameter("Voornaam", Inzending.Voornaam ?? (object) DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Achternaam", Inzending.Achternaam ?? (object) DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Emailadres", Inzending.Email ?? (object) DBNull.Value));
                    command.Parameters.Add(new SqliteParameter("Bestand", data ?? (object) DBNull.Value));

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
            var uploadedFile = Request.Form["uploadedFile"];

            _logger.LogInformation("Test");

            _logger.LogInformation(voornaam);
            _logger.LogInformation(achternaam);
            _logger.LogInformation(emailadres);
            _logger.LogInformation(uploadedFile);

            return RedirectToPage("Privacy");
        }
    }
}
