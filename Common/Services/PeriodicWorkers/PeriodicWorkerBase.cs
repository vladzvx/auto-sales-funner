using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services
{
    public class PeriodicWorker<TWorker> : IHostedService where TWorker:ISettings
    {
        private readonly TWorker settings;
        private readonly IDbContextFactory<ContactsContext> dbContextFactory;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly Thread WorkerThread;
        public PeriodicWorker(TWorker settings, IDbContextFactory<ContactsContext> dbContextFactory)
        {
            WorkerThread = new Thread(new ParameterizedThreadStart(worker));
            this.settings = settings;
            this.dbContextFactory = dbContextFactory;
        }

        public void worker(object cancellationToken)
        {
            if (cancellationToken is CancellationToken token)
            {
                bool executed = false;
                DateTime lastExecution = DateTime.UtcNow.AddDays(2);
                while (!token.IsCancellationRequested)
                {
                    DateTime dt = DateTime.UtcNow;
                    if (!executed && dt.Subtract(dt.Date) >= settings.Time)
                    {
                        try
                        {
                            if (settings.action!=null)
                                settings.action(dbContextFactory);
                            //ContactsWorker.CreateLinks(dbContextFactory, settings).Wait();
                        }
                        catch (Exception ex)
                        {

                        }
                        lastExecution = DateTime.UtcNow;
                        executed = true;
                    }
                    if (DateTime.UtcNow.Date.Subtract(lastExecution) > settings.Period)
                    {
                        executed = false;
                    }
                    Thread.Sleep(settings.WorkPeriod);
                }
            }
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            WorkerThread.Start(cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
