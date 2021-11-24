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
        private readonly ISettings settings;
        private readonly IDbContextFactory<ContactsContext> dbContextFactory;
        public EventController(ISettings settings, IDbContextFactory<ContactsContext> dbContextFactory)
        {
            this.settings = settings;
            this.dbContextFactory = dbContextFactory;

        }
        [HttpPost("recieve")]
        public async Task<string> RecieveEvent(CancellationToken token)
        {
            GetLead getLead = new GetLead();
            DealUpdate du = new DealUpdate(); 
            MoveLead ml = new MoveLead(); 
            CreateContact cc = new CreateContact(); 
            DealCreator dc = new DealCreator(); 
            using (StreamReader reader = new StreamReader(this.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                string data = await reader.ReadToEndAsync();
                respones = data;
                getLead.IdLead = "";
                await Utils.Requests.ExecuteGet(getLead.Create, null, async (str) => 
                {
                    using (var cont = dbContextFactory.CreateDbContext())
                    {
                        var res = await cont.Contacts.Where(item => item.Phone == "2222").ToListAsync();
                        if (res.Any())
                        {
                            await Utils.Requests.ExecuteGet(du.Create, null, null);
                            await Utils.Requests.ExecuteGet(ml.Create, null, null);
                            //что записать в БД?
                        }
                        else
                        {
                            await Utils.Requests.ExecuteGet(cc.Create, null, null);
                            await Utils.Requests.ExecuteGet(dc.Create, null, null);
                        }
                    }
                });
            }
            return "ok";
        }

        [HttpPost("get")]
        public async Task<string> GetEvent(CancellationToken token)
        {
            return respones;
        }
    }
}
