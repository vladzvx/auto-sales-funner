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
    public class CheckerSettings : ThreadSafeSettingsBase
    {
        private CheckShortLinkCreator dealCreator;
        private ReportClik report;

        public CheckerSettings(HeadersProcessor headersProcessor)
        {
            Time = new TimeSpan(0, 0, 0);
            this.WorkPeriod = new TimeSpan(0, 2, 0);
            this.Period = new TimeSpan(0, 2, 0);
            dealCreator = new CheckShortLinkCreator() { };
            report = new ReportClik();
            action = async (contextFactory) =>
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    IEnumerable<Contact> contacts = await context.Contacts
                        .Where(item => item.LinkId != null && !item.HasClick)
                        .ToListAsync();
                    foreach (var cont in contacts)
                    {
                        dealCreator.IdLink = cont.LinkId;
                        
                        await Requests.ExecuteGet(dealCreator.Create, headersProcessor.AddHeaders, async (res) =>
                        {
                            report.IdDeal = cont.ClientId.ToString();
                            cont.ClickDateTime = DateTime.UtcNow;
                            report.Date = string.Format("{0}.{1}.{2} {3}.{4}.{5}", cont.ClickDateTime.Day, cont.ClickDateTime.Month,
                                cont.ClickDateTime.Year, cont.ClickDateTime.Hour, cont.ClickDateTime.Minute, cont.ClickDateTime.Second);
                            await Requests.ExecuteGet(report.Create, headersProcessor.AddHeaders, async (re) => { });
                            cont.HasClick = true;
                            context.Contacts.Update(cont);
                        });



                    }
                    await context.SaveChangesAsync();
                }
            };
        }
    }
}
