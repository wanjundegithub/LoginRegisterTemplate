using ControlTemplate.ViewModels;

namespace ControlTemplate.Views
{
    /// <summary>
    /// ChildView.xaml 的交互逻辑
    /// </summary>
    public partial class ChildView 
    {
        public ChildView()
        {
            InitializeComponent();
            DataContext = new ChildViewModel();
        }
    }
}
