using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace hon3y.Pages
{
    public class Formulier2Model : PageModel
    {

        private readonly ILogger<Formulier2Model> _logger;

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Formulier2Model(ILogger<Formulier2Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        /*public void OnPost()
        {
            var voornaam = Request.Form["voornaam"];
            var achternaam = Request.Form["achternaam"];
            var emailadres = Request.Form["emailadres"];
            var uploadedFile = Request.Form["uploadedFile"];

            _logger.LogInformation("Test");

            //Console.WriteLine(voornaam, achternaam, emailadres);

            _logger.LogInformation(voornaam);
            _logger.LogInformation(achternaam);
            _logger.LogInformation(emailadres.ToString());
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

            using (var connection = (SqliteConnection) _connectionString)
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"";

                command.Parameters.AddWithValue(@"", Form.Voornaam);

                await command.ExecuteNonQueryAsync();
            }

        }
    }
}
