using ControlTemplate.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace ControlTemplate.Views
{
    /// <summary>
    /// AdminView.xaml 的交互逻辑
    /// </summary>
    public partial class AdminView :IViewFor<AdminViewModel>
    {
        public AdminView()
        {
            InitializeComponent();
            ViewModel = new AdminViewModel();
            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Users, v => v.ListBox_User.ItemsSource).DisposeWith(d);
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
                ViewModel = (AdminViewModel)value;
            }
        }

        public AdminViewModel ViewModel
        {
            get { return (AdminViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(AdminViewModel), typeof(AdminView));


    }
}
