using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    public class DealCreator : LinkCreatorBase
    {
        public string Title { get; init; }
        public string ContactId { get; init; }
        public string CompanyId { get; init; }
        public string ApiKey { get; init; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.item.add?entityTypeId={1}&fields[title]={2}&fields[categoryId]={3}&fields[stageId]={4}&fields[contactId]={5}&fields[companyId]={6}&fields[ufCrm2ShortLinkSms]={7}",
                Target, EntityTypeId, Title, CategoryId, StageId, ContactId, CompanyId, ApiKey);
        }
    }
}
