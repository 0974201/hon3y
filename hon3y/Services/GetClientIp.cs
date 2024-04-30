using System.Net;
using Microsoft.AspNetCore;

namespace hon3y.Services
{
    public class GetClientIp
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetClientIp(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string? GetIp()
        {
            return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
    }
}
