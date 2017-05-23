using System;

namespace GoMicro.Forex.Core
{
    public class FixerSettings : IFixerSettings
    {
        public FixerSettings() { }
        public string BaseUrl { get; set; }
        public string Currencies { get; set; }
        public Guid AppSecret { get; set; }
    }
}
