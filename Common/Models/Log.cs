using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Log
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string LogLevel { get; set; }
        public int ThreadId { get; set; }
        public string Message { get; set; }
    }
}
