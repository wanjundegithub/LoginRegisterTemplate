using ReactiveUI;

namespace ControlTemplate.ViewModels
{
    public  abstract class ViewModelBase : ReactiveObject,IActivatableViewModel
    {
        public  ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
