using Microsoft.EntityFrameworkCore;

namespace hon3y.Models
{
    public class AfspraakContext : DbContext
    {
        public AfspraakContext(DbContextOptions options) : base(options) 
        { 
            
        }
    }
}
