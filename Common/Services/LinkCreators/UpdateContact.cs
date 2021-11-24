using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class UpdateContact : LinkCreatorBase
    {
        public string Mail { get; init; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.contact.update?id=8&fields[EMAIL][0][VALUE]={1}", Target, Mail);
        }
    }
}
