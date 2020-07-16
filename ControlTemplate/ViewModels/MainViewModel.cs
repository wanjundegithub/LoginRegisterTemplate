using ControlTemplate.Interfaces;
using ReactiveUI;
using System;
using System.Reactive;

namespace ControlTemplate.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel(Lazy<IChildWindowAsyncSevice> childWindowAsyncSevice)
        {
            TestCommand = ReactiveCommand.Create(()=> Unit.Default);
            CloseCommand = ReactiveCommand.Create(() => Unit.Default);
            TestCommand.Subscribe(d =>
            {
                childWindowAsyncSevice.Value.ShowCustomChildWindowAsync();
            });
        }

        public ReactiveCommand<Unit,Unit> TestCommand { get; }

        public ReactiveCommand<Unit,Unit> CloseCommand { get; }
    }
}
