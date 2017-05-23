using System.Web.Http;
using GoMicro.Forex.Models;
using GoMicro.Forex.Services;
using System.Linq;
using System;

namespace GoMicro.Forex.WebApi.Controllers
{
    [RoutePrefix("api/v1/Convert")]
    public class ConvertController : ApiController
    {
        private readonly IRateService rateService;

        public ConvertController(IRateService Service)
        {
            rateService = Service;
        }
        
        [HttpPost]
        [Route("{Base}/{Amount:double}/{Target}")]
        public CommonResult Post(string @base, double amount, string target, DateTime? date = null)
        {
            try
            {
                Fixer fixer = rateService.Get(@base);//await GetFixerLatest(@base);
                double rate = fixer.rates.Where(r => r.Key == target).Select(r => r.Value).SingleOrDefault();
                double data = amount * rate;
                return new CommonResult(true, data.ToString());
            }
            catch (Exception e)
            {
                string m = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    m += " > InnerException > " + e.Message;
                }
                return new CommonResult(false, m);
            }

        }
    }
}
