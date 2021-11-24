using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class GetLead : LinkCreatorBase
    {
        public string IdLead { get; set; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.lead.get?id={1}", Target, IdLead);
        }
    }
}
