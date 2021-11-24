using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class UpdateContact2 : LinkCreatorBase
    {
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/12/ck6uqtx0ne2pm1xj/crm.contact.update?id=8&fields[UF_CRM_1637162427]=UF_CRM_COOKIES", Target);
        }
    }
}
