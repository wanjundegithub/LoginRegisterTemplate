using System.Threading.Tasks;

namespace ControlTemplate.Interfaces
{
    public interface IWindowSevice
    {
        void ShowWindow();
    }

    public interface IChildWindowAsyncSevice
    {
        Task ShowCustomChildWindowAsync(string title,object content);
    }
}
