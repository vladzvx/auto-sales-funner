using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class Options
    {
        public static string Target { get; } = Environment.GetEnvironmentVariable("Target") ?? "12/ck6uqtx0ne2pm1xj";
        public static string EntityTypeId { get; } = Environment.GetEnvironmentVariable("EntityTypeId") ?? "162";
        public static string StageId { get; } = Environment.GetEnvironmentVariable("StageId") ?? "DT162_2:NEW";
        public static string CategoryId { get; } = Environment.GetEnvironmentVariable("CategoryId") ?? "2.0";
        public static string Domain { get; } = Environment.GetEnvironmentVariable("Domain") ?? "l.osbb-kr.com.ua";
    }
}
