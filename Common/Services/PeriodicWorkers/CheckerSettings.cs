using Common.Models;
using Common.Services.DataBase;
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
    public class CheckerSettings : ThreadSafeSettingsBase
    {
        private CheckShortLinkCreator dealCreator;
        private ReportClik report;
        private readonly LogWriter logWriter;
        public CheckerSettings(HeadersProcessor headersProcessor, LogWriter logWriter)
        {
            this.logWriter = logWriter;
            Time = Options.GetTime("empty");
            this.WorkPeriod = new TimeSpan(0, 1, 0);
            this.Period = new TimeSpan(0, 0, 0);
            dealCreator = new CheckShortLinkCreator() { };
            report = new ReportClik();
            action = async (contextFactory) =>
            {
                logWriter.Log("Checker action started!");
                using (var context = contextFactory.CreateDbContext())
                {
                    logWriter.Log("Contacts requesting!");
                    IEnumerable<Contact> contacts = await context.Contacts
                        .Where(item => item.DealId != null && item.LinkId != null && !item.HasClick)
                        .ToListAsync();
                    logWriter.Log("Geted " + contacts.ToList().Count.ToString() + " contacts.");
                    foreach (var cont in contacts)
                    {
                        logWriter.Log("Processing contact: " + System.Text.Json.JsonSerializer.Serialize(cont));
                        dealCreator.IdLink = cont.LinkId;
                        
                        await Requests.ExecuteGet(dealCreator.Create, headersProcessor.AddHeaders, async (res) =>
                        {
                            JObject obj = JObject.Parse(res);
                            if (obj!=null&&obj.ContainsKey("clickStatistics"))
                            {
                                var obj2 = JObject.Parse(obj.GetValue("clickStatistics").ToString());
                                if (obj2 != null && obj2.ContainsKey("datasets"))
                                {
                                    var obj3 = JArray.Parse(obj2.GetValue("datasets").ToString());
                                    if (obj3!=null && obj3.Count > 0)
                                    {
                                        var obj4 = JObject.Parse(obj3[0].ToString());
                                        if (obj4 != null && obj4.ContainsKey("data"))
                                        {
                                            var obj5 = JArray.Parse(obj4.GetValue("data").ToString());
                                            if (obj5.Count > 0)
                                            {
                                                report.IdDeal = cont.DealId.ToString();

                                                DateTime tmp = DateTime.UtcNow;
                                                cont.ClickDateTime = tmp.ToString();
                                                report.Date = string.Format("{0}.{1}.{2} {3}:{4}:{5}", tmp.Day, tmp.Month,
                                                    tmp.Year, tmp.Hour, tmp.Minute, tmp.Second);
                                                await Requests.ExecuteGet(report.Create, headersProcessor.AddHeaders, async (re) => { });
                                                cont.HasClick = true;
                                                context.Contacts.Update(cont);
                                                logWriter.Log("Updating contact. New data: " + System.Text.Json.JsonSerializer.Serialize(cont));
                                            }
                                        }
                                    }
                                }
                            }



                        });



                    }
                    await context.SaveChangesAsync();
                    logWriter.Log("Changes saved");
                }
            };
        }
    }
}
