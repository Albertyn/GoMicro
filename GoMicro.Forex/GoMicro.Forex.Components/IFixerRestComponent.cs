using System.Net.Http;

namespace GoMicro.Forex.Components
{
    interface IFixerRestComponent
    {
        HttpResponseMessage GetFixerQuote(string IsoAlpha3Code);
    }
}