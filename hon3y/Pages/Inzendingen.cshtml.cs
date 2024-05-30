using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
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

        public void OnGet()
        {
        }

        [BindProperty]
        public Models.Inzendingen Inzending { get; set; } = new Models.Inzendingen();

        /*public void OnPost()
        {
            
        }*/

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            byte[] data = null;
            if (Request.Form.Files.Count > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await Request.Form.Files[0].CopyToAsync(stream);
                    data = stream.ToArray();
                }
            }

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
            
            var voornaam = Request.Form["voornaam"];
            var achternaam = Request.Form["achternaam"];
            var emailadres = Request.Form["emailadres"];
            var uploadedFile = Request.Form["uploadedFile"];

            _logger.LogInformation("Test");

            //Console.WriteLine(voornaam, achternaam, emailadres);

            _logger.LogInformation(voornaam);
            _logger.LogInformation(achternaam);
            _logger.LogInformation(emailadres.ToString());

            return RedirectToPage("Privacy");
        }
    }
}
