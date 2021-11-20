using Common.Interfaces;
using Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventRecieverController
    {
        private readonly ISettings settings;
        private readonly IDbContextFactory<ContactsContext> dbContextFactory;
        public EventRecieverController(ISettings settings, IDbContextFactory<ContactsContext> dbContextFactory)
        {
            this.settings = settings;
            this.dbContextFactory = dbContextFactory;

        }
        [HttpPost()]
        public async Task<string> RecieveWebhook(CancellationToken token)
        {
            return "ok";
        }
    }
}
