using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hon3y.Pages
{
    public class FormModel : PageModel
    {

        private readonly ILogger<FormModel> _logger;
        public string emailadres { get; set; }

        public FormModel(ILogger<FormModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        [IgnoreAntiforgeryToken] //dit hoort hier dus niet 
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
            _logger.LogInformation(emailadres); //pakt m niet for some reason. 
            _logger.LogInformation(telefoonnummer);
            _logger.LogInformation(reden);
            _logger.LogInformation(datum);
        }
    }
}
