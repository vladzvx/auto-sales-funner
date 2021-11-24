using Common.Interfaces;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.LinkCreators
{
    class CreateContact : LinkCreatorBase
    {
        public string Mail { get; init; }
        public string Phone { get; init; }
        public string Name { get; init; }
        public override string Create()
        {
            return string.Format("https://ecu-global.bitrix24.ua/rest/{0}/crm.contact.add?fields[NAME]={1}&fields[PHONE][0][VALUE]={2}&fields[EMAIL][0][VALUE]={3}", Target, Name, Phone, Mail);
        }
    }
}
