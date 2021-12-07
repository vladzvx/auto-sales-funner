using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class Options
    {
        public static string Target { get; } = Environment.GetEnvironmentVariable("API_KEY");
        public static string EntityTypeId { get; } = Environment.GetEnvironmentVariable("ENTITY_TYPE_ID") ?? "162";
        public static string StageId { get; } = Environment.GetEnvironmentVariable("NEW_STAGE_ID");
        public static string StageId2 { get; } = Environment.GetEnvironmentVariable("STAGE_ID2");
        public static string CategoryId { get; } = Environment.GetEnvironmentVariable("CATEGORY_ID") ?? "2.0";
        public static string Domain { get; } = Environment.GetEnvironmentVariable("DOMAIN"); 
        public static TimeSpan GetTime(string EnvName)
        {
            Regex reg = new Regex(@"(\d\d):(\d\d):(\d\d)");
            string val = Environment.GetEnvironmentVariable(EnvName);
            if (val != null)
            {
                Match match = reg.Match(val);
                if (match.Success)
                {
                    return new TimeSpan(ParseTime(match.Groups[1].Value), ParseTime(match.Groups[2].Value), ParseTime(match.Groups[3].Value));
                } 
            }
            return new TimeSpan(0, 0, 0);
        }

        private static int ParseTime(string time)
        {
            Regex timeReg = new Regex(@"^\d\d$");
            if (timeReg.IsMatch(time))
            {
                if (time[0] == '0')
                {
                    time = time.Remove(0);
                }
                int res = int.Parse(time);
                if (res < 24)
                {
                    return res;
                }
            }
            return 0;
        } 
    }
}
