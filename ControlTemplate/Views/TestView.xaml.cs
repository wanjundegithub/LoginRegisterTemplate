using ControlTemplate.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;


namespace ControlTemplate.Views
{
    /// <summary>
    /// TestView.xaml 的交互逻辑
    /// </summary>
    public partial class TestView 
    {
        public TestView()
        {
            InitializeComponent();
            var viewModel = new TestViewModel();
            ViewModel = viewModel;
            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, vm => vm.ShowCommand, v => v.Button_Show).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Show, v => v.TextBox_Show.Text).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.TestCommand, v => v.Button_Test).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Test, v => v.TextBox_Test.Text).DisposeWith(d);
            });
        }

    }
}
