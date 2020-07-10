using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ControlTemplate.Behaviors
{
    public class PasswordBehavior:Behavior<PasswordBox>
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBehavior), new PropertyMetadata(null,PasswordPropertyChangedBack));

        //密码更新
        private static void PasswordPropertyChangedBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            string password = (string)e.NewValue;
            if(passwordBox!=null&& password!=passwordBox.Password)
            {
                passwordBox.Password = password;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        //将PasswordBox密码赋值到依赖属性值
        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if(passwordBox!=null&&passwordBox.Password!=Password)
            {
                Password = passwordBox.Password;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }
}
