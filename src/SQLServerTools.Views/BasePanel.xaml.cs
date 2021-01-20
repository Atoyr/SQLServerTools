using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SQLServerTools.Views.Dialogs;
using SQLServerTools.ViewModels;

namespace SQLServerTools.Views
{
  /// <summary>
  /// Editor.xaml の相互作用ロジック
  /// </summary>
  public partial class BasePanel : UserControl
  {
    public BasePanel()
    {
      InitializeComponent();
    }

    private async void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
      var ctx = this.DataContext as BasePanelViewModel;
      var setting = new DbLoginDialogSettings()
      {
        IntegratedSecurityCheckBoxVisibility = Visibility.Visible,
        IntegratedSecurityCheckBoxText = "Windows認証",
        NegativeButtonVisibility = Visibility.Visible,
        NegativeButtonText = "キャンセル"
      };

      bool ok = false;
      string message = string.Empty;

      do
      {
        var result = await DbLoginDialogManager.ShowDbLoginDialog(setting, "SQLServerへ接続");

        if (result == null ) break;
        (ok, message) = ctx.UpdateDbContext(result.Servername, result.IntegratedSecurity, result.Username, result.Password);
        if (!ok)
        {
            var dialogSetting = new MetroDialogSettings()
            {
              AffirmativeButtonText = "OK",
              NegativeButtonText = "キャンセル"
            };
            MetroWindow window = (MetroWindow)Application.Current.MainWindow;

            var dialogResult = await window.ShowMessageAsync("接続に失敗しました", message,
                                                                     MessageDialogStyle.AffirmativeAndNegative, dialogSetting);
            if (dialogResult == MessageDialogResult.Negative || dialogResult == MessageDialogResult.Canceled)
            {
              break;
            }
        }
      }
      while (!ok);
    }

    private void DisconnectButton_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
