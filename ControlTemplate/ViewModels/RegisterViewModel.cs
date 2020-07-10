using ControlTemplate.Interfaces;
using ControlTemplate.Models;
using ControlTemplate.Views;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace ControlTemplate.ViewModels
{
    public class RegisterViewModel :ViewModelBase
    {
        public RegisterViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => Unit.Default);
            LoginCommand = ReactiveCommand.Create(() => Unit.Default);
            LoginCommand.Subscribe(d =>
            {
                WindowManager.ShowView(nameof(MainView));
            });

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
        public ReactiveCommand<Unit,Unit> CloseCommand { get; }

        public ReactiveCommand<Unit,Unit> LoginCommand { get; }

        public IObservable<Unit> Result => LoginCommand.Select(u => Unit.Default).Merge(CloseCommand);
    }
}
