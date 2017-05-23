using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using GoMicro.Forex.Core;
using GoMicro.Forex.Models;
using Newtonsoft.Json;

namespace GoMicro.Forex.Components
{
    class FixerRestComponent: IFixerRestComponent
    {
        private readonly IFixerSettings fixerSettings;
        private HttpClient httpClient = new HttpClient();
        public FixerRestComponent(IFixerSettings Settings)
        {
            fixerSettings = Settings;

            httpClient.BaseAddress = new Uri(fixerSettings.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public HttpResponseMessage GetFixerQuote(string IsoAlpha3Code)
        {
            string url = ($"fixerSettings.BaseUrl?latest?base=IsoAlpha3Code.ToUpper()");            
            return httpClient.GetAsync(url).GetAwaiter().GetResult();
        }
        private Fixer GetFixer(string IsoAlpha3Code)
        {
            HttpResponseMessage response = GetFixerQuote(IsoAlpha3Code);
            string responseString = response.Content.ToString();
            return JsonConvert.DeserializeObject<Fixer>(responseString);
        }
    }
}
