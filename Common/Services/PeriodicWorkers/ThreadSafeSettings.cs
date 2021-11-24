using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public abstract class ThreadSafeSettingsBase : ISettings
    {
        public object locker = new object();
        private TimeSpan salesCreatorPeriod;
        private TimeSpan salesCreatorTime;
        private TimeSpan salesCreatorWorkPeriod;
        private uint salesPerAction;
        public TimeSpan Period { get 
            {
                lock (locker)
                {
                    return salesCreatorPeriod;
                }
            }
            set 
            {
                lock (locker)
                {
                    salesCreatorPeriod = value;
                }
            } 
        }
        public TimeSpan Time
        {
            get
            {
                lock (locker)
                {
                    return salesCreatorTime;
                }
            }
            set
            {
                lock (locker)
                {
                    salesCreatorTime = value;
                }
            }
        }
        public TimeSpan WorkPeriod
        {
            get
            {
                lock (locker)
                {
                    return salesCreatorWorkPeriod;
                }
            }
            set
            {
                lock (locker)
                {
                    salesCreatorWorkPeriod = value;
                }
            }
        }
        public uint SalesPerAction
        {
            get
            {
                lock (locker)
                {
                    return salesPerAction;
                }
            }
            set
            {
                lock (locker)
                {
                    salesPerAction = value;
                }
            }
        }
        public virtual Action<IDbContextFactory<ContactsContext>> action { get; init; }
    }
}
