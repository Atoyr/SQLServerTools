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

      //var dbLoginDialogTask = this.ShowMetroDialogAsync<DbLoginDialog>(dbLoginDialogSettings);
      var dialog = new DbLoginDialog(window, setting);
      dialog.Title = "SQL Server Connection";
      return window.ShowMetroDialogAsync(dialog).ContinueWith(x =>
          {
          return dialog.WaitForButtonPressAsync().ContinueWith(y =>
              {
                window.HideMetroDialogAsync(dialog).Wait();
                return y;
                }).Unwrap();
          }).Unwrap();
    }
  }
}
