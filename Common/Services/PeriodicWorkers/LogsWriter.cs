using Common.Interfaces;
using Common.Models;
using Common.Services.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services
{
    public class LogsWriterWrapper : IHostedService
    {
        public LogWriter logsWriterCore;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public LogsWriterWrapper(LogWriter logsWriterCore)
        {
            this.logsWriterCore = logsWriterCore;
            ILinkCreator.LogWriter = logsWriterCore;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logsWriterCore.Start(cancellationTokenSource.Token);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource.Cancel();
        }
        
    }
}
