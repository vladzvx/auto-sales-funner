using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class DealUpdate : LinkCreatorBase
    {
        public string IdDeal { get; init; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.item.update?entityTypeId={1}&id={2}&fields[ufCrm2LidPrishel]=1&fields[ufCrm2IzFormy]=UF_CRM_FORMNAME&fields[ufCrm2DateVstrechi]=UF_CRM_UKAZHITEDATUV&fields[ufCrm2Ga]=UF_CRM_COOKIES", Target, EntityTypeId, IdDeal);        
        }
    }
}
