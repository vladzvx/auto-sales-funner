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
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactsContext(DbContextOptions<ContactsContext> options):base(options)
        {
            bool temp = Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasKey(c => c.Id);
        }
    }
}
