using System;
using Autofac;
using ControlTemplate;
using ControlTemplate.Views;
using ReactiveUI;
using Splat;
using Splat.Serilog;

namespace ControlTemplateProgram
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var container = new ContainerBuilder();
            container.RegisterModule<MainModule>();
            using (var builder = container.Build())
            {
                //Locator.CurrentMutable.InitializeSplat();
                //Locator.CurrentMutable.InitializeReactiveUI();
                //Locator.CurrentMutable.UseSerilogFullLogger();
                builder.Resolve<RegisterView>().Show();
            }
        }
    }
}
