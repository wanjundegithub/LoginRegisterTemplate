using Microsoft.Xaml.Behaviors;
using System.Windows;
using ControlTemplate.Adorners;
using System;
using System.Windows.Documents;
using System.Windows.Controls;

namespace ControlTemplate.Behaviors
{
    public class BorderBehavior:Behavior<FrameworkElement>
    {

        #region 附加IsHasBorder属性



        public bool IsHasBorder
        {
            get { return (bool )GetValue(IsHasBorderProperty); }
            set { SetValue(IsHasBorderProperty, value); }
        }

        private static readonly DependencyProperty IsHasBorderProperty =
            DependencyProperty.Register(nameof(IsHasBorder), typeof(bool), typeof(BorderBehavior), 
                new PropertyMetadata(false, OnIsHasBorderPropertyChangedCallback, OnIsHasBorderPropertyCoerceValueCallback));


        #endregion
        private static void OnIsHasBorderPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BorderBehavior).IsBorderPropertyChanged((bool)e.NewValue);
        }

        private static object OnIsHasBorderPropertyCoerceValueCallback(DependencyObject d, object value)
        {
            return (d as BorderBehavior).IsBorderPropertyCoerceChanged((bool)value);
        }

        private object IsBorderPropertyCoerceChanged(bool value)
        {
            if (AssociatedObject == null)
                return false;
            return value;
        }

        private void IsBorderPropertyChanged(bool value)
        {
            if(BorderAdorner!=null)
            {
                (BorderAdorner.Parent as AdornerLayer).Remove(BorderAdorner);
                BorderAdorner = null;
                return;
            }
            else
            {
                if (!value)
                    return;
                var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
                if (adornerLayer == null)
                    throw new Exception($"该元素没有装饰层");
                BorderAdorner = new BorderAdorner(AssociatedObject);
                adornerLayer.Add(BorderAdorner);
            }
        }

        private BorderAdorner BorderAdorner { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            CoerceValue(IsHasBorderProperty);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
    }
}
