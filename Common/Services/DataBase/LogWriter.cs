using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services.DataBase
{
    public class LogWriter
    {
        private readonly ConcurrentQueue<Log> logs = new ConcurrentQueue<Log>();
        private readonly IDbContextFactory<LogsContext> dbContextFactory;
        private readonly Thread workerThread;
        public LogWriter(IDbContextFactory<LogsContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
            workerThread = new Thread(new ParameterizedThreadStart(Worker));
        }
        private void Put(Log log)
        {
            logs.Enqueue(log);
        }

        public void Log(string message)
        {
            Put(new Log() { LogLevel = "Debug", Message = message, ThreadId = Thread.CurrentThread.ManagedThreadId, Timestamp = DateTime.UtcNow });
        }
        public void LogError(string message)
        {
            Put(new Log() { LogLevel = "Error", Message = message, ThreadId = Thread.CurrentThread.ManagedThreadId, Timestamp = DateTime.UtcNow });
        }
        public void LogError(Exception ex, string message = "")
        {
            if (message != "") message = message + "; ";
            Put(new Log() { LogLevel = "Error", Message = message+ex.GetType().ToString() + "; " + ex.Message, ThreadId = Thread.CurrentThread.ManagedThreadId, Timestamp = DateTime.UtcNow });
        }

        public void Start(CancellationToken cancellationToken)
        {
            workerThread.Start(cancellationToken);
        }

        public void Worker(object cancellationToken)
        {
            if (cancellationToken is CancellationToken ct)
            {
                while (!ct.IsCancellationRequested)
                {
                    try
                    {
                        using (LogsContext cnt = dbContextFactory.CreateDbContext())
                        {
                            int count = 0;
                            while (count < 1000 && logs.TryDequeue(out Log log))
                            {
                                cnt.Add(log);
                                count++;
                            }
                            cnt.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(300);
                }
            }
        }
    }
}
