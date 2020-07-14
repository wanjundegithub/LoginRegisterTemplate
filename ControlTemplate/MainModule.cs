using Autofac;
using ControlTemplate.ViewModels;
using ControlTemplate.Views;

namespace ControlTemplate
{
    public class MainModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => new RegisterView(c.Resolve<RegisterViewModel>())).AsSelf().SingleInstance();
            builder.Register(c => new RegisterViewModel()).AsSelf().SingleInstance();
        }
    }
}
