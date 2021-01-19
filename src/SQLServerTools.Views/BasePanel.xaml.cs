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
      var setting = new DbLoginDialogSettings();
      var result = await DbLoginDialogManager.ShowDbLoginDialog(setting, "SQLServerへ接続");
      var ctx = this.DataContext as BasePanelViewModel;
      if (!ctx.UpdateDbContext(result.Servername, result.IntegratedSecurity, result.Username, result.Password))
      {
        result = await DbLoginDialogManager.ShowDbLoginDialog(setting, "SQLServerへ接続");
        ctx.UpdateDbContext(result.Servername, result.IntegratedSecurity, result.Username, result.Password);
      }
    }

    private void DisconnectButton_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
