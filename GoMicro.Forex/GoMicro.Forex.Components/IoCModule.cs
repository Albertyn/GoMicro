using Autofac;
using Module = Autofac.Module;
using GoMicro.Forex.Components.Repositories;

namespace GoMicro.Forex.Components
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FixerRepository>().As<IFixerRepository>();
            builder.RegisterType<FixerDbComponent>().As<IFixerDbComponent>();
            builder.RegisterType<FixerRestComponent>().As<IFixerRestComponent>();
        }
    }
}
