using System;
using System.Net.Http;
using GoMicro.Forex.Models;
using GoMicro.Forex.Components;
using System.Threading.Tasks;

namespace GoMicro.Forex.Services
{
    class RateService : IRateService
    {
        private readonly IFixerDbComponent dbComponent;
        private readonly IFixerRestComponent restComponent;
        public RateService(IFixerDbComponent DbComp, IFixerRestComponent RestComp)
        {
            dbComponent = DbComp;
            restComponent = RestComp;
        }

        public HttpResponseMessage FixerQuoteByBase(string IsoAlpha3Code)
        {
            return restComponent.GetFixerQuote(IsoAlpha3Code);
        }


        public Fixer Get(string IsoAlpha3Code)
        {
            return dbComponent.GetFixerLatest(IsoAlpha3Code).GetAwaiter().GetResult();
        }
        public Task<Fixer> GetAsync(string IsoAlpha3Code)
        {
            return dbComponent.GetFixerLatest(IsoAlpha3Code);
        }


        public void Add(Fixer model)
        {
            dbComponent.Add(model);
        }

        public void Delete(string _id)
        {
            throw new NotImplementedException();
        }


    }
}
