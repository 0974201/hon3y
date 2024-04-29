using Microsoft.EntityFrameworkCore;

namespace hon3y.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions options) : base( options )
        {

        }
    }
}
