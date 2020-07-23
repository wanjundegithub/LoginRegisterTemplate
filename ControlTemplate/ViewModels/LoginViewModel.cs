using ControlTemplate.Interfaces;
using ControlTemplate.Models;
using ControlTemplate.Validations;
using ReactiveUI;
using System;
using System.Collections;
using System.Reactive;
using System.Reactive.Linq;

namespace ControlTemplate.ViewModels
{
    public class LoginViewModel :ViewModelBase
    {
        public LoginViewModel(IWindowSevice windowSevice/*,IOtherWindowSevice otherWindowSevice*/)
        {
            this.WhenAnyValue(d => d.Password).Subscribe(s =>
            {
                var result = Validation.TextValidation.Validate(s);
                if (result.IsValid)
                {
                    PasswordError = null;
                    IsRightPasswordBorder = true;
                    return;
                }
                PasswordError = result.Errors;
                IsRightPasswordBorder = false;
            });
            this.WhenAnyValue(d => d.Account).Subscribe(s =>
            {
                var r = Validation.TextValidation.Validate(s);
                if (r.IsValid)
                {
                    AccountError = null;
                    IsRightAccountBorder = true;
                    return;
                }             
                AccountError = r.Errors;
                IsRightAccountBorder = false;
            });
            var canExecute = this.WhenAnyValue(d => d.Account).CombineLatest(this.WhenAnyValue(d => d.Password), (account, password)=>
            {
                var accountResult = Validation.TextValidation.Validate(account);
                var passwordResult = Validation.TextValidation.Validate(password);
                if (accountResult.IsValid && passwordResult.IsValid&&!string.IsNullOrEmpty(account)&&!string.IsNullOrEmpty(password))
                    return true;
                return false;
            });
            CloseCommand = ReactiveCommand.Create(() =>
            {
                return true;
            });
            LoginCommand = ReactiveCommand.Create(() =>
            {
                ILoginData login = new LoginData(Help.Path);
                if(!login.ValidateUser(Account,Password))
                    return false;
                return true;
            },canExecute);
            LoginCommand.Subscribe(d =>
            {
                if (d)
                    windowSevice.ShowCustomWindow();     
            });
            RegisterCommand = ReactiveCommand.Create(() => Unit.Default);
            RegisterCommand.Subscribe(d =>
            {
                //otherWindowSevice.ShowOtherCustomWindow();
            });
        }


        private string _account = string.Empty;

        public string Account
        {
            get
            {
                return _account;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _account, value);
            }
        }
        private string _password = string.Empty;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _password, value);
            }
        }

        private IEnumerable _accountError;

        public IEnumerable AccountError
        {
            get 
            { 
                return _accountError;
            }
            set 
            {
                this.RaiseAndSetIfChanged(ref _accountError, value);
            }
        }


        private IEnumerable _passwordError;

        public IEnumerable PasswordError
        {
            get
            {
                return _passwordError;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _passwordError, value);
            }
        }

        private bool _isRightAccountBorder ;

        public bool IsRightAccountBorder
        {
            get
            {
                return _isRightAccountBorder;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isRightAccountBorder, value);
            }
        }

        private bool _isRightPasswordBorder ;

        public bool IsRightPasswordBorder
        {
            get
            {
                return _isRightPasswordBorder;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isRightPasswordBorder, value);
            }
        }
        public ReactiveCommand<Unit,bool> CloseCommand { get; }

        public ReactiveCommand<Unit,bool> LoginCommand { get; }

        public ReactiveCommand<Unit,Unit> RegisterCommand { get; }

        public IObservable<bool> Result => LoginCommand.Select(b => b).Merge(CloseCommand);


       
    }
}
