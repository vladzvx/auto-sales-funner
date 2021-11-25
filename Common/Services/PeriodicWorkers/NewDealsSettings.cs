using Common.Models;
using Common.Services.LinkCreators;
using Common.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
            SalesPerAction = 40;
            this.Time = new TimeSpan(0, 0, 0);
            this.WorkPeriod = new TimeSpan(0, 1, 0);
            dealCreator = new DealCreator() { };
            action = async (contextFactory) =>
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    IEnumerable<Contact> contacts = await context.Contacts
                        .Where(item => item.DealId == null)
                        .Take((int)this.SalesPerAction)
                        .ToListAsync();
                    foreach (var cont in contacts)
                    {
                        dealCreator.Title = cont.Phone;
                        dealCreator.CompanyId = cont.CompanyId;
                        dealCreator.ContactId = cont.ClientId;
                        dealCreator.ApiKey = cont.ShortLink;
                        await Requests.ExecuteGet(dealCreator.Create, headersProcessor.AddHeaders, async(resp) =>
                        {
                            JObject obj = JObject.Parse(resp);
                            if (obj.ContainsKey("result"))
                            {
                                var obj2 = JObject.Parse(obj.GetValue("result").ToString());
                                if (obj2.ContainsKey("item"))
                                {
                                    var obj3 = JObject.Parse(obj2.GetValue("item").ToString());
                                    if (obj3.ContainsKey("id"))
                                    {
                                        cont.DealId = obj3["id"].ToString();
                                        context.Contacts.Update(cont);
                                    }
                                }
                            }

                        });
                    }
                    await context.SaveChangesAsync();
                }
            };
        }
    }
}
