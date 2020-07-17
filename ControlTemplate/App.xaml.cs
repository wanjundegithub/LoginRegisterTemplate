using Autofac;
using ControlTemplate.Views;
using ReactiveUI;
using Splat;
using Splat.Serilog;
using System.Windows;

namespace ControlTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = new ContainerBuilder();
            container.RegisterModule<MainModule>();
            var builder = container.Build();
            using (var scope=builder.BeginLifetimeScope())
            {
                Locator.CurrentMutable.InitializeSplat();
                Locator.CurrentMutable.InitializeReactiveUI();
                Locator.CurrentMutable.UseSerilogFullLogger();
                scope.Resolve<LoginView>().Show();
            }
        }
    }
}
