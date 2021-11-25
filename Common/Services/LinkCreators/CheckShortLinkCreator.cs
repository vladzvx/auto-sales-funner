using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class CheckShortLinkCreator : LinkCreatorBase
    {
        public string IdLink { get; set; }
        public override string Create()
        {
            return string.Format("https://api-v2.short.cm/statistics/link/{0}",
                IdLink);
        }
    }
}
