using Common.Models;
using Common.Services.LinkCreators;
using Common.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.PeriodicWorkers
{
    public class NewDealsSettings : ThreadSafeSettingsBase
    {
        private DealCreator dealCreator;

        public NewDealsSettings(HeadersProcessor headersProcessor)
        {
            this.Time = new TimeSpan(0, 0, 0);
            this.WorkPeriod = new TimeSpan(0, 1, 0);
            dealCreator = new DealCreator() { };
            action = async (contextFactory) =>
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    IEnumerable<Contact> contacts = await context.Contacts
                        .Where(item => item.LinkId == null)
                        .Take((int)this.SalesPerAction)
                        .ToListAsync();
                    foreach (var cont in contacts)
                    {
                        await Requests.ExecuteGet(dealCreator.Create, headersProcessor.AddHeaders, async(resp) =>
                        {
                            cont.LinkId = resp;
                            context.Contacts.Update(cont);
                        });
                    }
                    await context.SaveChangesAsync();
                }
            };
        }
    }
}
