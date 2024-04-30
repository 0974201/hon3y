using Microsoft.EntityFrameworkCore;

namespace hon3y.Models
{
    public class UploadContext : DbContext
    {
        public UploadContext(DbContextOptions options) : base(options) 
        { 

        }
    }
}