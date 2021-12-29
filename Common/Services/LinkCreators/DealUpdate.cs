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
        public string IdDeal { get; set; }
        public string UF_CRM_COOKIES { get; set; }
        public string UF_CRM_FORMNAME { get; set; }
        public string UF_CRM_UKAZHITEDATUV { get; set; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.item.update?entityTypeId={1}&id={2}&fields[ufCrm2LidPrishel]=1&fields[ufCrm2IzFormy]={3}&fields[ufCrm2DateVstrechi]={4}&fields[ufCrm2Ga]={5}", Target, EntityTypeId, IdDeal, UF_CRM_FORMNAME,UF_CRM_UKAZHITEDATUV, UF_CRM_COOKIES);        
        }
    }
}
