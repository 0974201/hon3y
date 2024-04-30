using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hon3y.Pages
{
    public class Formulier2Model : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var textvak1 = Request.Form["textvak1"];
            var textvak2 = Request.Form["textvak2"];
            var emailadres = Request.Form["emailadres"];
            var uploadedFile = Request.Form["uploadedFile"];
            return null;
        }
    }
}
