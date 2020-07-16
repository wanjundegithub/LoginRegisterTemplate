using Autofac;
using ControlTemplate.Interfaces;
using ControlTemplate.ViewModels;
using ControlTemplate.Views;
using System;

namespace ControlTemplate
{
    public class MainModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => new MainViewModel(c.Resolve<Lazy<IChildWindowAsyncSevice>>())).AsSelf().SingleInstance();
            builder.Register(c => new MainView(c.Resolve<MainViewModel>())).As<IWindowSevice>().As<IChildWindowAsyncSevice>().AsSelf().SingleInstance();
            builder.Register(c => new RegisterViewModel(c.Resolve<IWindowSevice>())).AsSelf().SingleInstance();
            builder.Register(c => new RegisterView(c.Resolve<RegisterViewModel>())).AsSelf().SingleInstance();
           
        }
    }
}
