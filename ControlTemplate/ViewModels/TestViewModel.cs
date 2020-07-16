

using ReactiveUI;
using System;
using System.Reactive;

namespace ControlTemplate.ViewModels
{
    public class TestViewModel:ViewModelBase
    {
        public TestViewModel()
        {
            ShowCommand = ReactiveCommand.Create(() => Unit.Default);
            ShowCommand.Subscribe(d =>
            {
                Show = "Show you";
            });
            TestCommand = ReactiveCommand.Create(() => Unit.Default);
            TestCommand.Subscribe(d =>
            {
                Test = "Test you";
            });
        }

        private string _show = string.Empty;

        public string Show
        {
            get
            {
                return _show;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _show, value);
            }
        }

        private string _test = string.Empty;

        public string Test
        {
            get
            {
                return _test;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _test, value);
            }
        }

        public ReactiveCommand<Unit,Unit> ShowCommand { get; }

        public ReactiveCommand<Unit,Unit>TestCommand { get; }
    }
}
