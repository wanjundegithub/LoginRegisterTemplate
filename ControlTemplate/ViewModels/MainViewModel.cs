using ControlTemplate.Models;
using ControlTemplate.Views;
using ReactiveUI;
using System;
using System.Reactive;

namespace ControlTemplate.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel()
        {
            TestCommand = ReactiveCommand.Create(()=> Unit.Default);
            TestCommand.Subscribe(d =>
            {
                WindowManager.ShowView(nameof(TestView));
            });
        }

        public ReactiveCommand<Unit,Unit> TestCommand { get; }
    }
}
