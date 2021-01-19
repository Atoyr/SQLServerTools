using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SQLServerTools.Views.Dialogs
{
  public static class DbLoginDialogManager
  {
    public static Task<DbLoginDialogData> ShowDbLoginDialog(DbLoginDialogSettings setting, string dialogTitle)
    {
      MetroWindow window = (MetroWindow)Application.Current.MainWindow;

      var dialog = new DbLoginDialog(window, setting);
      dialog.Title = dialogTitle;
      return window.ShowMetroDialogAsync(dialog).ContinueWith(x =>
          {
          return dialog.WaitForButtonPressAsync().ContinueWith(y =>
              {
                window.HideMetroDialogAsync(dialog);
                return y;
                }, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
          }, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
    }
  }
}
