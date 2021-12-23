using Common.Interfaces;
using Common.Services.DataBase;
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
        private readonly LogWriter logWriter;
        public PeriodicWorker(TWorker settings, IDbContextFactory<ContactsContext> dbContextFactory,LogWriter logWriter)
        {
            WorkerThread = new Thread(new ParameterizedThreadStart(worker));
            this.settings = settings;
            this.logWriter = logWriter;
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
                            logWriter.Log("Starting periodic action!");
                            if (settings.action!=null)
                                settings.action(dbContextFactory);
                            logWriter.Log("Periodic action ended!");
                        }
                        catch (Exception ex)
                        {
                            logWriter.LogError(ex,"Starting periodic failed!");
                        }
                        lastExecution = DateTime.UtcNow;
                        executed = true;
                    }
                    if (DateTime.UtcNow.Subtract(lastExecution) > settings.Period)
                    {
                        logWriter.Log("Switching executed status!");
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
