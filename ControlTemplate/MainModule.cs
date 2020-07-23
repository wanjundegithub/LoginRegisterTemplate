using Autofac;
using ControlTemplate.Interfaces;
using ControlTemplate.ViewModels;
using ControlTemplate.Views;
using ReactiveUI;
using Splat;
using System;

namespace ControlTemplate
{
    public class MainModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //由viewmodel解析出view
            Locator.CurrentMutable.Register<IViewFor<TestViewModel>>(() => new TestView());

            //builder.Register(c => new RegisterViewModel()).AsSelf();
            //builder.Register(c => new RegisterView()).As<IOtherWindowSevice>();
            builder.Register(c => new TestViewModel()).AsSelf(); ;
            builder.Register(c => new MainViewModel(c.Resolve<Lazy<IChildWindowAsyncSevice>>(),c.Resolve<Func<TestViewModel>>())).AsSelf();
            builder.Register(c => new MainView(c.Resolve<MainViewModel>())).As<IWindowSevice>().As<IChildWindowAsyncSevice>().AsSelf().SingleInstance();
            builder.Register(c => new LoginViewModel(c.Resolve<IWindowSevice>()/*,c.Resolve<IOtherWindowSevice>()*/)).AsSelf().SingleInstance();
            builder.Register(c => new LoginView(c.Resolve<LoginViewModel>())).As<IViewFor<LoginViewModel>>().AsSelf().SingleInstance();
           
        }
    }
}
