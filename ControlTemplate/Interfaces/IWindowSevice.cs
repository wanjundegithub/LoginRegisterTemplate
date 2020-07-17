using System;
using System.Threading.Tasks;

namespace ControlTemplate.Interfaces
{
    public interface IWindowSevice
    {
        void ShowCustomWindow();
    }

    public interface IOtherWindowSevice
    {
        void ShowOtherCustomWindow();
    }
    public interface IChildWindowAsyncSevice
    {
        Task ShowCustomChildWindowAsync<T>(string title,IHasObservableResult<T> content);
    }


    public interface IHasObservableResult<T>
    {
        IObservable<T> Result { get; }
    }
}
