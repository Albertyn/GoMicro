using GoMicro.Forex.Models;
using System.Net.Http;

namespace GoMicro.Forex.Services
{
    public interface IRateService
    {
        HttpResponseMessage FixerQuoteByBase(string IsoAlpha3Code);

        void Add(Fixer model);

        Fixer Get(string IsoAlpha3Code);

        void Delete(string _id);
    }
}
