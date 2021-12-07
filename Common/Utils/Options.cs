﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
