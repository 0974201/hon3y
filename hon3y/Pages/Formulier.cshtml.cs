using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql; //google ado net. eventueel periodiek kijken of db nog correct werkt, anders code schrijven dat db zichzelf ""repareert""
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hon3y.Pages
{
    public class FormModel : PageModel
    {

        private readonly ILogger<FormModel> _logger;

        public FormModel(ILogger<FormModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        //[IgnoreAntiforgeryToken]
        public void OnPost()
        {
            var voornaam = Request.Form["voornaam"];
            var achternaam = Request.Form["achternaam"];
            var emailadres = Request.Form["emailadres"];
            var telefoonnummer = Request.Form["telefoonnummer"];
            var reden = Request.Form["afspraakreden"];
            var datum = Request.Form["datum"];

            _logger.LogInformation("Test");

            Console.WriteLine(voornaam, achternaam, emailadres, telefoonnummer, reden, datum);

            _logger.LogInformation(voornaam);
            _logger.LogInformation(achternaam);
            _logger.LogInformation(emailadres.ToString());
            _logger.LogInformation(telefoonnummer);
            _logger.LogInformation(reden);
            _logger.LogInformation(datum);
        }
    }
}
