using hon3y.Models;
using Microsoft.EntityFrameworkCore;

namespace hon3y.Data
{
    public class FormulierenContext : DbContext
    {
        public FormulierenContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Afspraak> Afspraken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Upload>(entity =>
            {
                entity.Property(e => e.UploadId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

                entity.Property(e => e.Voornaam)
                .HasColumnName("Voornaam");

                entity.Property(e => e.Achternaam)
                .HasColumnName("Achternaam");

                entity.Property(e => e.Email)
                .HasColumnName("Email");

                entity.Property(e => e.UploadedFile)
                .HasColumnName("Geuploade bestand");
            });*/

            modelBuilder.Entity<Login>().ToTable("Login").Property(e => e.LoginId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Upload>().ToTable("Upload").Property(e => e.UploadId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Afspraak>().ToTable("Afspraken").Property(e => e.AfspraakId).ValueGeneratedOnAdd();
        }

    }
}