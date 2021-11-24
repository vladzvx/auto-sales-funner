using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class ReportClik : LinkCreatorBase
    {
        public string IdDeal { get; set; }
        public string Date { get; set; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.item.update?entityTypeId={1}&id={2}&fields[ufCrm2DateTimeCliclShortLink]={3}", Target, EntityTypeId, IdDeal,Date);        
        }
    }
}
