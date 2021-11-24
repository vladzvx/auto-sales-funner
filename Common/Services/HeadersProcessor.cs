using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class HeadersProcessor
    {
        public string AuthToken { get; init; }
        public HeadersProcessor()
        {
            AuthToken = "token";
        }

        public void AddHeaders(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AuthToken);
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        }
    }
}
