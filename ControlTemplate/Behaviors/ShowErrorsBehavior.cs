using System;
using System.Collections;
using System.Windows;
using System.Windows.Documents;
using ControlTemplate.Adroners;
using Microsoft.Xaml.Behaviors;

namespace ControlTemplate.Behaviors
{
    public sealed class ShowErrorsBehavior : Behavior<FrameworkElement>
    {
        #region Errors


        public IEnumerable Errors
        {
            get { return GetValue(ErrorsProperty) as IEnumerable; }
            set { SetValue(ErrorsProperty, value); }
        }

        public static readonly DependencyProperty ErrorsProperty =
            DependencyProperty.Register(nameof(Errors), typeof(IEnumerable), typeof(ShowErrorsBehavior), new PropertyMetadata(null, ErrorsChanged, CoerceErrors));


        private static void ErrorsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            (dependencyObject as ShowErrorsBehavior).ErrorsChangedCallback(e.NewValue as IEnumerable);
        }

        private static object CoerceErrors(DependencyObject dependencyObject, object value)
        {
            return (dependencyObject as ShowErrorsBehavior).CoerceErrorsCallback(value);
        }

        private void ErrorsChangedCallback(IEnumerable errors)
        {
            if (errors == null)
            {
                if (ErrorsAdorner != null)
                {
                    (ErrorsAdorner.Parent as AdornerLayer).Remove(ErrorsAdorner);
                    ErrorsAdorner = null;
                }
            }
            else
            {
                if (ErrorsAdorner == null)
                {
                    //返回视觉树中指定Visual之上的第一个装饰层
                    var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
                    if (adornerLayer == null)
                    {
                        throw new Exception("该元素没有AdornerLayer");
                    }
                    ErrorsAdorner = new ErrorsAdorner(AssociatedObject);
                    adornerLayer.Add(ErrorsAdorner);
                }
                ErrorsAdorner.SetErrors(Errors);
            }
        }

        private object CoerceErrorsCallback(object value)
        {
            if (AssociatedObject == null)
            {
                return Errors;
            }
            return value;
        }

        #endregion

        private ErrorsAdorner ErrorsAdorner { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoadCallback;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoadCallback;
        }
        private void AssociatedObjectLoadCallback(object sender, RoutedEventArgs e)
        {
            CoerceValue(ErrorsProperty);
        }

    }
}
