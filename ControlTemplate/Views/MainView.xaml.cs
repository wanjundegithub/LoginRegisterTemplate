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
    public partial class MainView :IViewFor<MainViewModel>, IWindowSevice,IChildWindowAsyncSevice
    {
        
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            viewModel.CloseCommand.Subscribe(d =>  
            {
                Close();
            });
            ViewModel = viewModel;
            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, vm => vm.TestCommand, v => v.Button_Test).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CloseCommand, v => v.Button_Exit).DisposeWith(d);
            });
        }


       object IViewFor.ViewModel
        {
            get
            {
                return ViewModel;
            }
            set
            {
                ViewModel = (MainViewModel)value;
            }
        }

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(MainViewModel), typeof(MainView));


        public async Task ShowCustomChildWindowAsync()
        {
            await this.ShowChildWindowAsync(new ChildView(), OverlayFillBehavior.WindowContent);
        }

        public void ShowWindow()
        {
            this.Show();
        }


    }
}
