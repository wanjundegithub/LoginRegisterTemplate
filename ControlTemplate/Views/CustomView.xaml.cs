﻿using ControlTemplate.ViewModels;
using MahApps.Metro.SimpleChildWindow;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace ControlTemplate.Views
{
    /// <summary>
    /// CustomView.xaml 的交互逻辑
    /// </summary>
    public partial class CustomView :IViewFor<CustomViewModel>
    {
        public CustomView(string title,Func<object> content,IObservable<Unit> getClose)
        {
            InitializeComponent();
            ViewModel = new CustomViewModel(title, content.Invoke());
            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Title, v => v.Title).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Content, v => v.ViewModelViewHost_Content.ViewModel);
                getClose.Select(u=>Unit.Default).Subscribe(u => Close(CloseReason.None)).DisposeWith(d);
            });
        }

        

        public CustomViewModel ViewModel
        {
            get { return (CustomViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel 
        { 
            get
            {
                return ViewModel;
            }
            set
            {
                ViewModel = (CustomViewModel)value;
            }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(CustomViewModel), typeof(CustomView));

    }
}
