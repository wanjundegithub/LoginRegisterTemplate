using System;
using System.Windows.Input;

namespace ControlTemplate.Interfaces
{
    public interface IOperationCommand
    {
        ICommand Command { get; }

        //IObservable<bool> CanExecute { get; }
    }
}
