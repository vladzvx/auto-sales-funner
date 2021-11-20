using Common.Interfaces;
using Common.Services.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Common.Services
{
    public class LinksClickChecker : PeriodicWorkerBase
    {
        private readonly ISettings settings;
        private readonly IDbContextFactory<ContactsContext> dbContextFactory;
        public LinksClickChecker(ISettings settings, IDbContextFactory<ContactsContext> dbContextFactory)
        {
            this.settings = settings;
            this.dbContextFactory = dbContextFactory;
        }
        public override void worker(object cancellationToken)
        {
            if (cancellationToken is CancellationToken token)
            {
                while (!token.IsCancellationRequested)
                {
                    ContactsWorker.SetLinksClicked(dbContextFactory, settings).Wait();
                    Thread.Sleep(settings.SalesCreatorWorkPeriod);
                }
            }
        }
    }
}
