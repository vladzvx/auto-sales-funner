using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.DataBase
{
    public class ContactsWorker
    {
        public static async Task CreateLinks(IDbContextFactory<ContactsContext> dbContextFactory, ISettings settings)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                IEnumerable<Contact> contacts = await context.Contacts
                    .Where(item => item.LinkId == null)
                    .Take((int)settings.SalesPerAction)
                    .ToListAsync();
                foreach (var cont in contacts)
                {
                    cont.LinkId = "1234";
                    context.Contacts.Update(cont);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task SetLinksClicked(IDbContextFactory<ContactsContext> dbContextFactory, ISettings settings)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                IEnumerable<Contact> contacts = await context.Contacts
                    .Where(item => item.LinkId != null && !item.HasClick)
                    .Take((int)settings.SalesPerAction)
                    .ToListAsync();
                foreach (var cont in contacts)
                {
                    cont.HasClick = true;
                    cont.ClickDateTime = DateTime.UtcNow;
                    context.Contacts.Update(cont);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
