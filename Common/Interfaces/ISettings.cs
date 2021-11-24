using Common.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ISettings
    {
        public TimeSpan WorkPeriod { get; set; }
        public TimeSpan Period { get; set; }
        public TimeSpan Time { get; set; }
        public uint SalesPerAction { get; set; }
        public Action<IDbContextFactory<ContactsContext>> action { get; }
    }
}
