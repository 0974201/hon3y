using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hon3y.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Ip;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private string GetIp()
        {
            Ip = HttpContext.Connection.RemoteIpAddress.ToString();

            return Ip;
        }

        public void OnGet()
        {
            Console.WriteLine(GetIp());
        }
    }
}
