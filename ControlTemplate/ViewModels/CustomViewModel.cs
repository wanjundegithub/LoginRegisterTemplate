using System;
using System.Collections.Generic;
using System.Text;

namespace ControlTemplate.ViewModels
{
    public class CustomViewModel:ViewModelBase
    {
        public string Title { get; }

        public object Content { get; }

        public CustomViewModel(string title,object content)
        {
            Title = title;
            Content = content;
        }
    }
}
