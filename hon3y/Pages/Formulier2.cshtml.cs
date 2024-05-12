using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace hon3y.Pages
{
    public class Formulier2Model : PageModel
    {

        private readonly ILogger<Formulier2Model> _logger;

        public Formulier2Model(ILogger<Formulier2Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var voornaam = Request.Form["voornaam"];
            var achternaam = Request.Form["achternaam"];
            var emailadres = Request.Form["emailadres"];
            var uploadedFile = Request.Form["uploadedFile"];

            _logger.LogInformation("Test");

            Console.WriteLine(voornaam, achternaam, emailadres);

            _logger.LogInformation(voornaam);
            _logger.LogInformation(achternaam);
            _logger.LogInformation(emailadres.ToString());
        }
    }
}
