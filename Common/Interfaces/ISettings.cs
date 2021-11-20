using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ISettings
    {
        public TimeSpan SalesCreatorWorkPeriod { get; set; }
        public TimeSpan SalesCreatorPeriod { get; set; }
        public TimeSpan SalesCreatorTime { get; set; }
        public uint SalesPerAction { get; set; }
    }
}
