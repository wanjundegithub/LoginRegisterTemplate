using ControlTemplate.Interfaces;
using ControlTemplate.ViewModels;
using MahApps.Metro.SimpleChildWindow;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using static MahApps.Metro.SimpleChildWindow.ChildWindowManager;

namespace ControlTemplate.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class RenderView :IViewFor<RenderViewModel>, IWindowSevice,IChildWindowAsyncSevice
    {
        
        public RenderView(RenderViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Closed += RenderView_Closed;
            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, vm => vm.TestCommand, v => v.Button_Test).DisposeWith(d);
            });
        }

        private void RenderView_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        object IViewFor.ViewModel
        {
            get
            {
                return ViewModel;
            }
            set
            {
                ViewModel = (RenderViewModel)value;
            }
        }

        public RenderViewModel ViewModel
        {
            get { return (RenderViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(RenderViewModel), typeof(RenderView));


       

        public void ShowCustomWindow()
        {
            this.Show();
        }

        public async Task ShowCustomChildWindowAsync<T>(string title,  IHasObservableResult<T> content)
        {
            await this.ShowChildWindowAsync(new CustomView(title,content,o=>content.Result.Select(r=>Unit.Default)), OverlayFillBehavior.WindowContent);
        }

    }
}
