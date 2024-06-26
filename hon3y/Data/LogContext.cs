﻿using hon3y.Models;
using Microsoft.EntityFrameworkCore;

namespace hon3y.Data
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Logs> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logs>().ToTable("Login").Property(e => e.LogId).ValueGeneratedOnAdd();
        }
    }
}