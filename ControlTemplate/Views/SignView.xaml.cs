using ControlTemplate.Interfaces;
using ControlTemplate.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;

namespace ControlTemplate.Views
{
    /// <summary>
    /// Sign.xaml 的交互逻辑
    /// </summary>
    public partial class SignView :IViewFor<SignViewModel>,IOtherWindowSevice
    { 
        public SignView()
        {
            InitializeComponent();
            Closed+=new EventHandler(SignViewClosed);
            ViewModel = new SignViewModel();
            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.RegisterAccount, v => v.TextBox_RegisterAccount.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.RegisterPassword, v => v.PasswordBehavior_RegisterPassword.Password).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.RegisterComfirmPassword, v => v.PasswordBehavior_RegisterComfirmPassword.Password).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.RegisterAccountError, v => v.ShowErrorsBehavior_RegisterAccount.Errors).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.RegisterPasswordError, v => v.ShowErrorsBehavior_RegisterPassword.Errors).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.RegisterComfirmPasswordError, v => v.ShowErrorsBehavior_RegisterComfirmPassword.Errors).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.RegisterCommand, v => v.Button_Register).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CloseCommand, v => v.Button_Close).DisposeWith(d);
                ViewModel.Result.Subscribe(d => Close()).DisposeWith(d);
            });
        }

        private static SignView _instance;

        public static SignView Instance
        {
            get
            {
                if (_instance == null)
                    return new SignView();
                return _instance;
            }
        }


        object IViewFor.ViewModel
        {
            get
            {
                return ViewModel;
            }
            set
            {
                ViewModel = (SignViewModel)value;
            }
        }

        public SignViewModel ViewModel
        {
            get { return (SignViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(SignViewModel), typeof(SignView));

        public void ShowOtherCustomWindow()
        {
            Instance.Show();
        }

        private void SignViewClosed(object sender, EventArgs e)
        {
            _instance = null;
            this.Close();
        }
    }
}
