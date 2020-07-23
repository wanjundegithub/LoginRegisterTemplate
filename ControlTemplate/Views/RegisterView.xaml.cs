using ControlTemplate.Interfaces;
using ControlTemplate.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;

namespace ControlTemplate.Views
{
    /// <summary>
    /// Sign.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView :IViewFor<RegisterViewModel>,IOtherWindowSevice
    { 
        public RegisterView()
        {
            InitializeComponent();
            //Closed+=new EventHandler(SignViewClosed);
            ViewModel = new RegisterViewModel();
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
                ViewModel.Result.Subscribe(d=>
                {
                    if(d)
                    {
                        Close();
                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(this, "错误", "已存在相同用户名");
                    }
                }).DisposeWith(d);
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
                ViewModel = (RegisterViewModel)value;
            }
        }

        public RegisterViewModel ViewModel
        {
            get { return (RegisterViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(RegisterViewModel), typeof(RegisterView));

        public void ShowOtherCustomWindow()
        {
            var view = new RegisterView();
            view.Show();
        }

    }
}
