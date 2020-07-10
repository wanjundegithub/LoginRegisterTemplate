using ControlTemplate.Models;
using ControlTemplate.ViewModels;
using System;

namespace ControlTemplate.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView 
    {
        public RegisterView(RegisterViewModel viewModel)
        {
            InitializeComponent();
            WindowManager.RegisterView<MainView>(nameof(MainView));
            viewModel.Result.Subscribe(d =>
            {
                Close();
            });
            DataContext = viewModel;
        }

    }
}
