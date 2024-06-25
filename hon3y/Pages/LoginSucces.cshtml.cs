using hon3y.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace hon3y.Pages
{
    public class LoginSuccesModel : PageModel
    {
        private readonly ILogger<LoginSuccesModel> _logger;

        public LoginSuccesModel(ILogger<LoginSuccesModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
