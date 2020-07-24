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
    public class RegisterViewModel:ViewModelBase
    {
        public RegisterViewModel()
        {
            this.WhenAnyValue(d => d.RegisterAccount).Subscribe(d =>
              {
                  var r = Validation.TextValidation.Validate(d);
                  if(r.IsValid)
                  {
                      RegisterAccountError = null;
                      return;
                  }
                  RegisterAccountError = r.Errors;
              });
            this.WhenAnyValue(d => d.RegisterPassword).Subscribe(d =>
              {
                  var r = Validation.TextValidation.Validate(d);
                  if (r.IsValid)
                  {
                      RegisterPasswordError = null;
                      return;
                  }
                  RegisterPasswordError = r.Errors;
              });
            this.WhenAnyValue(d => d.RegisterComfirmPassword).Subscribe(d =>
              {
                  var r = Validation.TextValidation.Validate(d);
                  if(r.IsValid)
                  {
                      RegisterComfirmPasswordError = null;
                      return;
                  }
                  RegisterComfirmPasswordError = r.Errors;
              });
            var canExecuted = this.WhenAnyValue(d => d.RegisterAccount).CombineLatest(this.WhenAnyValue(d => d.RegisterPassword), 
                this.WhenAnyValue(d => d.RegisterComfirmPassword),(registerAccount, registerPassword, registerConfirmPassword) =>
                {
                    var r1 = Validation.TextValidation.Validate(registerAccount);
                    var r2 = Validation.TextValidation.Validate(registerPassword);
                    var r3 = Validation.TextValidation.Validate(registerConfirmPassword);
                    if (r1.IsValid&&r2.IsValid&&r3.IsValid&&registerPassword ==registerConfirmPassword
                    &&!string.IsNullOrEmpty(registerAccount)&&!string.IsNullOrEmpty(registerPassword)&&!string.IsNullOrEmpty(registerConfirmPassword))
                        return true;
                    return false;
                });
            _login = new LoginData(Help.Path);
            RegisterCommand = ReactiveCommand.Create(() =>
            {
                if(_login.IsExistUser(RegisterAccount))
                {
                    return false;
                }
                else
                {
                    _login.AddUser(RegisterAccount, RegisterPassword);
                    return true;
                }
            }, canExecuted);
            //测试专用
            DeleteCommand = ReactiveCommand.Create<string,bool>(s =>
              {
                  if (_login.DeleteUser(s))
                      return true;
                  return false;
              });
            CloseCommand = ReactiveCommand.Create(() =>
            {
                return true;
            });
        }

        private ILoginData _login { get; set; }

        private string _registerAccount = string.Empty;

        public string RegisterAccount
        {
            get
            {
                return _registerAccount;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _registerAccount, value);
            }
        }

        private string _registerPassword = string.Empty;

        public string RegisterPassword
        {
            get
            {
                return _registerPassword;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _registerPassword, value);
            }
        }

        private string _registerComfirmPassword = string.Empty;

        public string RegisterComfirmPassword
        {
            get
            {
                return _registerComfirmPassword;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _registerComfirmPassword, value);
            }
        }

        private IEnumerable _registerAccountError;

        public IEnumerable RegisterAccountError
        {
            get
            {
                return _registerAccountError;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _registerAccountError, value);
            }
        }

        private IEnumerable _registerPasswordError;

        public IEnumerable RegisterPasswordError
        {
            get
            {
                return _registerPasswordError;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _registerPasswordError, value);
            }
        }

        private IEnumerable _registerCobfirmPasswordError;

        public IEnumerable RegisterComfirmPasswordError
        {
            get
            {
                return _registerCobfirmPasswordError;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _registerCobfirmPasswordError, value);
            }
        }

        public ReactiveCommand<Unit,bool> RegisterCommand { get; }

        public ReactiveCommand<Unit,bool> CloseCommand { get; }

        public ReactiveCommand<string,bool> DeleteCommand { get; }

        public IObservable<bool> Result => CloseCommand.Select(d => d).Merge(RegisterCommand);

    }
}
