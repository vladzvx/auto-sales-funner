using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public class LogsContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LogsContext(DbContextOptions<LogsContext> options):base(options)
        {
            bool temp = Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasKey(c => c.Id);
        }
    }
}
