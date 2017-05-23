using Autofac;
using Module = Autofac.Module;

namespace GoMicro.Forex.Services
{
    class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RateService>().As<IRateService>();
        }
    }
}
