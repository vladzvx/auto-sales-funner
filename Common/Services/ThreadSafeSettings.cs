using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public class ThreadSafeSettings : ISettings
    {
        public object locker = new object();
        private TimeSpan salesCreatorPeriod;
        private TimeSpan salesCreatorTime;
        private TimeSpan salesCreatorWorkPeriod;
        private uint salesPerAction;
        public TimeSpan SalesCreatorPeriod { get 
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
        public TimeSpan SalesCreatorTime
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
        public TimeSpan SalesCreatorWorkPeriod
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
    }
}
