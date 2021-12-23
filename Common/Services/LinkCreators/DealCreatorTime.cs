using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    public class DealCreatorTime : LinkCreatorBase
    {
        public string Title { get; set; }
        public string ContactId { get; set; }
        public string CompanyId { get; init; }
        public string ApiKey { get; set; }
        public string Time { get; set; }
        public string UF_CRM_COOKIES { get; set; }
        public string UF_CRM_FORMNAME { get; set; }
        public string UF_CRM_UKAZHITEDATUV { get; set; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.item.add?entityTypeId={1}&fields[title]={2}&fields[categoryId]={3}&fields[stageId]={4}&fields[contactId]={7}&fields[ufCrm2ShortLinkSms]={5}&fields[ufCrm2LidPrishel]=1&fields[ufCrm2DateTimeCliclShortLink]={6}&fields[ufCrm2IzFormy]={8}&fields[ufCrm2DateVstrechi]={9}&fields[ufCrm2Ga]={10}",
                Target, EntityTypeId, Title, CategoryId, Options.StageId2, ApiKey,Time, ContactId, UF_CRM_FORMNAME, UF_CRM_UKAZHITEDATUV, UF_CRM_COOKIES);
        }
    }
}
