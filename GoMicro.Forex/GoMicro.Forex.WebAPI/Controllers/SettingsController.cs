using GoMicro.Forex.Models;
using GoMicro.Forex.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;

namespace GoMicro.Forex.WebApi.Controllers
{
    [RoutePrefix("api/Settings")]
    public class SettingsController : ApiController
    {
        private readonly IFixerSettings fixerSettings;

        public SettingsController(IFixerSettings Settings)
        {
            fixerSettings = Settings;
        }

        [HttpGet]
        [Route("Ping")]
        public CommonResult Ping() { return new CommonResult(true,"Pong"); }

        // GET: api/Settings
        [HttpGet]
        public IFixerSettings Get() { return fixerSettings; }

        // GET: api/Settings/Spread
        [HttpGet]
        [Route("Spread")]
        public string[] Spread()
        {
            string host = Request.RequestUri.Authority;
            Random random = new Random();
            List<string> L = new List<string>();

            foreach (string c in fixerSettings.Currencies.Split(','))
                foreach (string r in fixerSettings.Currencies.Split(','))
                    if (c != r) L.Add(host + "/api/Convert/" + c 
                        + "/" + (random.NextDouble() * (1000 - 1) + 1).ToString(CultureInfo.InvariantCulture) 
                        + "/" + r);

            return L.ToArray();
        }
    }
}
