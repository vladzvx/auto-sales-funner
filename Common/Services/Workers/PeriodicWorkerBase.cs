using Common.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services
{
    public abstract class PeriodicWorkerBase : IHostedService
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly Thread WorkerThread;
        public PeriodicWorkerBase()
        {
            WorkerThread = new Thread(new ParameterizedThreadStart(worker));
        }

        public abstract void worker(object cancellationToken);


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
