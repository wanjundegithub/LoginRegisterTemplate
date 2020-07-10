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
            using (var builder = container.Build())
            {
                Locator.CurrentMutable.InitializeSplat();
                Locator.CurrentMutable.InitializeReactiveUI();
                Locator.CurrentMutable.UseSerilogFullLogger();
                builder.Resolve<RegisterView>().Show();
            }
        }
    }
}
