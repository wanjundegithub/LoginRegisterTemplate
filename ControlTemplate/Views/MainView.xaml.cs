using ControlTemplate.Models;
using ControlTemplate.ViewModels;


namespace ControlTemplate.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView 
    {
        public MainView()
        {
            InitializeComponent();
            WindowManager.RegisterView<TestView>(nameof(TestView));
            DataContext = new MainViewModel();
        }
    }
}
