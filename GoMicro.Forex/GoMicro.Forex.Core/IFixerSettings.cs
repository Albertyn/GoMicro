using System;

namespace GoMicro.Forex.Core
{
    public interface IFixerSettings
    {
        string BaseUrl { get; set; }
        string Currencies { get; set; }

        Guid AppSecret { get; set; }
    }
}
