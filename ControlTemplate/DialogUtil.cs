using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ControlTemplate
{
    public static class DialogUtil
    {
        private static MetroWindow CustomWindow;

        private static Stack<CustomDialog> CustomDialogs = new Stack<CustomDialog>();

        static DialogUtil()
        {
            CustomWindow = Application.Current.MainWindow as MetroWindow;
        }

        public static  async Task ShowMessageAsync(string title,string message)
        {
            await CustomWindow.ShowMessageAsync(title, message);
        }

        public static async Task ShowCustomAsync(string title,object content,MetroDialogSettings settings)
        {
            CustomDialog customDialog = new CustomDialog(settings??CustomWindow.MetroDialogOptions);
            customDialog.Title = title;
            customDialog.Content = content;
            CustomDialogs.Push(customDialog);
            await CustomWindow.ShowMetroDialogAsync(customDialog);
            await customDialog.WaitUntilUnloadedAsync();
        }

        public static async void CloseCustomAsync()
        {
            if(CustomDialogs.Count>0)
            {
                CustomDialog dialog = CustomDialogs.Pop();
                dialog.Content = null;
                dialog.DialogSettings.AnimateHide = false;
                await CustomWindow.HideMetroDialogAsync(dialog);
            }
        }
    }
}
