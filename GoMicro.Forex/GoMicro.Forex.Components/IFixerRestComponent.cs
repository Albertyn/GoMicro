using System.Net.Http;

namespace GoMicro.Forex.Components
{
    public interface IFixerRestComponent
    {
        HttpResponseMessage GetFixerQuote(string IsoAlpha3Code);
    }
}