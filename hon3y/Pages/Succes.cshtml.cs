using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace hon3y.Pages
{
    public class SuccesModel : PageModel
    {
        private readonly ILogger<SuccesModel> _logger;

        public SuccesModel(ILogger<SuccesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
