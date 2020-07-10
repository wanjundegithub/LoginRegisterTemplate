using System;
using System.Collections;
using System.Windows;

namespace ControlTemplate.Models
{
    public static class WindowManager
    {
        private static Hashtable _registerViews = new Hashtable();
        
        public static void RegisterView<T>(string key)
        {
            if (_registerViews.ContainsKey(key))
                return;
            _registerViews.Add(key, typeof(T));
        }


        public static void RemoveView(string key)
        {
            if (!_registerViews.ContainsKey(key))
                return;
            _registerViews.Remove(key);
        }

        public static void ShowView(string key)
        {
            if (!_registerViews.ContainsKey(key))
                return;
            var view = (Window)Activator.CreateInstance((Type)_registerViews[key]);
            view.Show();
        }
    }
}
