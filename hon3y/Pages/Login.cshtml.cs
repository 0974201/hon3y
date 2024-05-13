using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hon3y.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;   

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public void OnPost()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"]; 

            _logger.LogInformation("Test");

            _logger.LogInformation(email);
            _logger.LogInformation(password);

        }
    }
}
