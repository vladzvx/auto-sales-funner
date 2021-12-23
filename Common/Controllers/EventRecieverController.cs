using Common.Interfaces;
using Common.Services;
using Common.Services.LinkCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        public static string respones = "no resp";
        public static string paramts = "no params";
        private readonly IDbContextFactory<ContactsContext> dbContextFactory;
        public EventController(IDbContextFactory<ContactsContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        [HttpPost("recieve1")]
        public async Task<string> RecieveEvent(CancellationToken token)
        {
            GetLead getLead = new GetLead();
            UpdateContact uc = new UpdateContact();
            DealUpdate du = new DealUpdate();
            MoveLead ml = new MoveLead();
            CreateContact cc = new CreateContact();
            DealCreator dc = new DealCreator();
            DealCreatorTime dct = new DealCreatorTime();
            getLead.IdLead = Request.Form["data[FIELDS][ID]"];
            ml.IdLead = Request.Form["data[FIELDS][ID]"];
            await Utils.Requests.ExecuteGet(getLead.Create, null, async (str) =>
            {
                respones = str;
                JObject obj1 = JObject.Parse(str);
                if (obj1["error"] != null) return;
                var phone = obj1["result"]["PHONE"][0]["VALUE"].ToString().Replace("(","").
                Replace(")", "").Replace(" ", "").Replace("-", "").Replace("\"", "");
                var email = obj1["result"]["EMAIL"][0]["VALUE"].ToString(); ;
                var name = obj1["result"]["NAME"].ToString(); ;
                var temp4 = obj1["result"]["UF_CRM_COOKIES"].ToString();
                var temp5 = obj1["result"]["UF_CRM_UKAZHITEDATUV"].ToString();
                var temp6 = obj1["result"]["UF_CRM_FORMNAME"].ToString();
                var temp7 = obj1["result"]["UTM_TERM"].ToString();

                using (var cont = dbContextFactory.CreateDbContext())
                {
                    var res = await cont.Contacts.Where(item => item.Phone == phone).ToListAsync();
                    bool debug = false;
                    if (debug||res.Any())
                    {
                        du.UF_CRM_COOKIES = temp4;
                        du.UF_CRM_UKAZHITEDATUV = temp5;
                        du.UF_CRM_FORMNAME = temp6;
                        uc.Mail = email.ToString();
                        await Utils.Requests.ExecuteGet(du.Create, null, null);
                        await Utils.Requests.ExecuteGet(uc.Create, null, null);
                        await Utils.Requests.ExecuteGet(ml.Create, null, null);;
                    }
                    else
                    {
                        cc.Mail = email;
                        cc.Name = name;
                        cc.Phone = phone;
                        await Utils.Requests.ExecuteGet(cc.Create, null, async (str)=> 
                        {
                            JObject obj1 = JObject.Parse(str);
                            dct.ContactId = obj1["result"].ToString();
                        });
                        dct.Title = name + " " + phone;
                        DateTime dt = DateTime.UtcNow;

                        dct.UF_CRM_COOKIES = temp4;
                        dct.UF_CRM_UKAZHITEDATUV = temp5;
                        dct.UF_CRM_FORMNAME = temp6;

                        dct.ApiKey = temp7;
                        dct.Time = string.Format("{0}.{1}.{2} {3}:{4}:{5}", dt.Day,dt.Month,dt.Year,dt.Hour,dt.Minute,dt.Second);
                        await Utils.Requests.ExecuteGet(dct.Create, null, async (str)=> 
                        {
                            var eee = str;
                        });
                    }
                }
            });
            return "ok";
        }

        [HttpPost("get")]
        public async Task<string> GetEvent(CancellationToken token)
        {
            if (respones == "") respones = "empty";
            return respones??"null";
        }

        [HttpPost("test")]
        public async Task<string> Test(CancellationToken token)
        {
            return System.Text.Json.JsonSerializer.Serialize(new TestLinkCreator());
        }

        [HttpPost("get2")]
        public async Task<string> GetEvent2(CancellationToken token)
        {
            return paramts;
        }
    }
}
