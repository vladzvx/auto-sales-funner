using Common.Interfaces;
using Common.Services;
using Common.Services.LinkCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            if (Request.Form.TryGetValue("[FIELDS][ID]", out var t))
            {
                respones = t.ToArray()[1];
            }
            
             
            //GetLead getLead = new GetLead();
            //DealUpdate du = new DealUpdate(); 
            //MoveLead ml = new MoveLead(); 
            //CreateContact cc = new CreateContact(); 
            //DealCreator dc = new DealCreator(); 
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
