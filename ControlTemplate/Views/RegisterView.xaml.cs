using ControlTemplate.ViewModels;
using System;
using ReactiveUI;
using System.Windows;
using System.Reactive.Disposables;
using ControlTemplate.Models;
using ControlTemplate.Interfaces;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using MahApps.Metro.SimpleChildWindow;
using static MahApps.Metro.SimpleChildWindow.ChildWindowManager;
using System.Reactive.Linq;
using System.Reactive;

namespace ControlTemplate.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView :IViewFor<RegisterViewModel>
    {
        public RegisterView(RegisterViewModel viewModel)
        {
           
            InitializeComponent();
            viewModel.Result.Subscribe(d =>
            {
                Close();
            });
            ViewModel = viewModel;
            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Account, v => v.TextBox_Account.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Password, v => v.PasswordBehavior_Password.Password).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.AccountError, v => v.ShowErrorsBehavior_Account.Errors).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.PasswordError, v => v.ShowErrorsBehavior_Password.Errors).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.IsRightAccountBorder, v => v.BorderBehavior_Account.IsHasBorder).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.IsRightPasswordBorder, v => v.BorderBehavior_Password.IsHasBorder).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CloseCommand, v => v.Button_Exit).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.Button_Login).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.RegisterCommand, v => v.Button_Register).DisposeWith(d);
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

        public async Task ShowCustomChildWindowAsync(string title,object content)
        {
            await this.ShowChildWindowAsync(new CustomView(title,content), OverlayFillBehavior.WindowContent);
        }

       
    }
}
