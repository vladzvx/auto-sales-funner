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
        [HttpPost("recieve")]
        public async Task<string> RecieveEvent(CancellationToken token)
        {
            //210
            //foreach (var v in Request.Form.Keys)
            //{
            //    respones += v + ":" + Request.Form[v]+";";
            //}

            GetLead getLead = new GetLead();
            UpdateContact uc = new UpdateContact();
            DealUpdate du = new DealUpdate();
            MoveLead ml = new MoveLead();
            CreateContact cc = new CreateContact();
            DealCreator dc = new DealCreator();
            getLead.IdLead = Request.Form["data[FIELDS][ID]"];
            ml.IdLead = Request.Form["data[FIELDS][ID]"];
            await Utils.Requests.ExecuteGet(getLead.Create, null, async (str) =>
            {
                JObject obj1 = JObject.Parse(str);


                var phone = obj1["result"]["PHONE"][0]["VALUE"].ToString().Replace("(","").
                Replace(")", "").Replace(" ", "").Replace("-", "").Replace("\"", "");
                var email = obj1["result"]["EMAIL"][0]["VALUE"].ToString(); ;
                var name = obj1["result"]["NAME"].ToString(); ;
                var temp4 = obj1["result"]["UF_CRM_COOKIES"].ToString();
                var temp5 = obj1["result"]["UF_CRM_UKAZHITEDATUV"].ToString();
                var temp6 = obj1["result"]["UF_CRM_FORMNAME"].ToString();

                using (var cont = dbContextFactory.CreateDbContext())
                {
                    var res = await cont.Contacts.Where(item => item.Phone == phone).ToListAsync();
                    if (res.Any())
                    {
                        du.UF_CRM_COOKIES = temp4;
                        du.UF_CRM_UKAZHITEDATUV = temp5;
                        du.UF_CRM_FORMNAME = temp6;
                        uc.Mail = email.ToString();
                        await Utils.Requests.ExecuteGet(du.Create, null, null);
                        await Utils.Requests.ExecuteGet(uc.Create, null, null);
                        await Utils.Requests.ExecuteGet(ml.Create, null, null);;
                        //что записать в БД?
                    }
                    else
                    {
                        cc.Mail = email;
                        cc.Name = name;
                        cc.Phone = phone;
                        await Utils.Requests.ExecuteGet(cc.Create, null, null);
                        //await Utils.Requests.ExecuteGet(dc.Create, null, null);
                    }
                }
            });
            //using (StreamReader reader = new StreamReader(this.Request.Body))
            //{
            //    try
            //    {
            //        paramts = this.Request.ContentType;
            //        string data = await reader.ReadToEndAsync();
            //        respones = data;
            //        getLead.IdLead = "";
            //        await Utils.Requests.ExecuteGet(getLead.Create, null, async (str) =>
            //        {
            //            using (var cont = dbContextFactory.CreateDbContext())
            //            {
            //                var res = await cont.Contacts.Where(item => item.Phone == "2222").ToListAsync();
            //                if (res.Any())
            //                {
            //                    await Utils.Requests.ExecuteGet(du.Create, null, null);
            //                    await Utils.Requests.ExecuteGet(ml.Create, null, null);
            //                    //что записать в БД?
            //                }
            //                else
            //                {
            //                    await Utils.Requests.ExecuteGet(cc.Create, null, null);
            //                    await Utils.Requests.ExecuteGet(dc.Create, null, null);
            //                }
            //            }
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        paramts = Newtonsoft.Json.JsonConvert.SerializeObject(ex);
            //    }

            //}
            return "ok";
        }

        [HttpPost("get")]
        public async Task<string> GetEvent(CancellationToken token)
        {
            if (respones == "") respones = "empty";
            return respones??"null";
        }

        [HttpPost("get2")]
        public async Task<string> GetEvent2(CancellationToken token)
        {
            return paramts;
        }
    }
}
