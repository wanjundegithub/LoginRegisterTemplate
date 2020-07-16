using ControlTemplate.Interfaces;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace ControlTemplate.ViewModels
{
    public class TestViewModel:ViewModelBase,IHasObservableResult<Unit>
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
            CloseCommand = ReactiveCommand.Create(() => Unit.Default);
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

        public ReactiveCommand<Unit,Unit> CloseCommand { get; }

        #region implement IHasObservableResult<Unit>
        IObservable<Unit> IHasObservableResult<Unit>.Result => CloseCommand.Select(r => Unit.Default);

        #endregion
    }
}
