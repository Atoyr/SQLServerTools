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
  public partial class StatsPanel : UserControl
  {
    public StatsPanel()
    {
      InitializeComponent();
    }

    public async void ComboBox_SelectionChangedEvent(object sender, SelectionChangedEventArgs e) 
    {
      var ctx = this.DataContext as StatsPanelViewModel;
      bool ok = false;
      string message = string.Empty;

      (ok, message) = ctx.FetchStats();
      if (!ok)
      {
        var dialogSetting = new MetroDialogSettings()
        {
          AffirmativeButtonText = "OK",
          NegativeButtonText = "Cancel"
        };
        MetroWindow window = (MetroWindow)Application.Current.MainWindow;

        var dialogResult = await window.ShowMessageAsync("Throw Exception", message,
            MessageDialogStyle.AffirmativeAndNegative, dialogSetting);
        return;
      }
    }
  }
}


