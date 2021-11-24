using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class Requests
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task ExecuteGet(Func<string> linkGen, Action<HttpRequestMessage> headersSetter=null, Func<string,Task> responseProcessor=null)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, linkGen()))
            {
                if (headersSetter != null) headersSetter(requestMessage);
                var resp = await httpClient.SendAsync(requestMessage);
                string res = await resp.Content.ReadAsStringAsync();
                if (responseProcessor != null) await responseProcessor(res);
            }
        }
    }
}
