using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Controls.Primitives;
using System.Reflection;
using Prism.Ioc;
using Prism.Unity;
using Unity;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SQLServerTools.Views.Dialogs;
using SQLServerTools.ViewModels;
using SQLServerTools.Config;

namespace SQLServerTools.Views
{
  /// <summary>
  /// Editor.xaml の相互作用ロジック
  /// </summary>
  public partial class MainPanel : UserControl
  {
    private bool isHandlingPanelViewModelsChanged = false;
    protected IList<TabControl> tabControls = new List<TabControl>();

    private MenuItem PART_ConnectMenuItem = new MenuItem();
    private MenuItem PART_DisconnectMenuItem = new MenuItem();
    private IUnityContainer Container;

    public MainPanel()
    {
      InitializeComponent();
      Container = Prism.Ioc.ContainerLocator.Container.GetContainer();
      DataContextChanged += DataContextChangedEventHandler;
      PART_ConnectMenuItem.Header = "Connect";
      PART_ConnectMenuItem.Click += ConnectButton_Click;
      PART_ConnectMenuItem.VerticalAlignment = VerticalAlignment.Bottom;

      PART_DisconnectMenuItem.Header = "Disconnect";
      PART_DisconnectMenuItem.Click += DisconnectButton_Click;
      PART_DisconnectMenuItem.VerticalAlignment = VerticalAlignment.Bottom;
      PART_DisconnectMenuItem.IsEnabled = false;

    }

    private TabControl NewTab()
    {
      var t = new TabControl();
      tabControls.Add(t);
      return t;
    }

    private TabItem AddTabItem(string contentTitle, UIElement element)
    {
      return AddTabItem(0, contentTitle, element);
    }

    private TabItem AddTabItem(int index, string contentTitle, UIElement element)
    {
      if (tabControls.Count < index )
      {
        return null;
      }
      if (tabControls.Count == 0)
      {
        var t = NewTab();
        Grid.SetRow(t, 0);
        Grid.SetColumn(t, 0);
        PART_MainGrid.Children.Add(t);
      }
      var tab = tabControls[index];
      var item = new TabItem();
      item.Header = contentTitle;
      item.Content = element;
      var i = tab.Items.Add(item);
      tab.SelectedIndex = i;

      return item;
    }

    private void vsplit()
    {
      if (tabControls.Count == 0)
      {

      }
      // splitter設置
      var splitColumnDefinition = new ColumnDefinition();
      splitColumnDefinition.Width = new GridLength(3);
      PART_MainGrid.ColumnDefinitions.Add(splitColumnDefinition);
      var splitter = new GridSplitter();
      Grid.SetRow(splitter, 0);
      Grid.SetColumn(splitter, PART_MainGrid.ColumnDefinitions.Count - 1);
      PART_MainGrid.Children.Add(splitter);
      
      // element設置
      var cd = new ColumnDefinition();
      PART_MainGrid.ColumnDefinitions.Add(cd);
      var t = NewTab();
      Grid.SetRow(t, 0);
      Grid.SetColumn(t, PART_MainGrid.ColumnDefinitions.Count - 1);
      PART_MainGrid.Children.Add(t);
    }

    private void DataContextChangedEventHandler(object sender, DependencyPropertyChangedEventArgs e)
    {
      Type type = DataContext.GetType();
      if (!isHandlingPanelViewModelsChanged && type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Any(x => x.Name == "PanelViewModels"))
      {
         PropertyInfo prop = type.GetProperty("PanelViewModels");
         object value = prop.GetValue(DataContext);
         var vms = value as INotifyCollectionChanged;
         if (vms != null)
         {
           vms.CollectionChanged += PanelViewModelsCollectionChangedEventHandler;
           isHandlingPanelViewModelsChanged = true;
         }
      }
    }

    private async void PanelViewModelsCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          foreach (var n in e.NewItems)
          {
            UserControl uc;
            var name = n.GetType().Name.Replace("ViewModel","");

            try 
            {
              switch (name) 
              {
                case "TablePanel":
                  uc = new TablePanel();
                  break;
                default:
                  return;
              }
            }
            catch (Exception exception)
            {
              var dialogSetting = new MetroDialogSettings()
              {
                AffirmativeButtonText = "OK",
                NegativeButtonText = "Cancel"
              };
              MetroWindow window = (MetroWindow)Application.Current.MainWindow;

              var dialogResult = await window.ShowMessageAsync("Throw Exception", exception.Message,
                  MessageDialogStyle.AffirmativeAndNegative, dialogSetting);
              return;
            }
            uc.DataContext = n;
            var title = "new";
            if (n.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Any(x => x.Name == "Title"))
            {
              PropertyInfo prop = n.GetType().GetProperty("Title");
              var titleProp = prop.GetValue(n);
              title = (string)titleProp.GetType().GetProperty("Value").GetValue(titleProp);
            }

            AddTabItem(0, title, uc);
          }
          break;
        case NotifyCollectionChangedAction.Remove:
          break;
      }
    }

    private async void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
      var ctx = this.DataContext as MainPanelViewModel;
      var config = Container.Resolve<Config.DbConnection>();
      var setting = new DbLoginDialogSettings()
      {
        IntegratedSecurityCheckBoxVisibility = Visibility.Visible,
        // IntegratedSecurityCheckBoxText = "Windows認証",
        NegativeButtonVisibility = Visibility.Visible,
        // NegativeButtonText = "キャンセル"
        ServerNames = config.ServerNames,
      };

      bool ok = false;
      string message = string.Empty;

      do
      {
        var result = await DbLoginDialogManager.ShowDbLoginDialog(setting, "Connect SQL Server");

        if (result == null ) break;
        (ok, message) = ctx.UpdateDbContext(result.Servername, result.IntegratedSecurity, result.Username, result.Password);
        if (!ok)
        {
            var dialogSetting = new MetroDialogSettings()
            {
              AffirmativeButtonText = "OK",
              NegativeButtonText = "Cancel"
            };
            MetroWindow window = (MetroWindow)Application.Current.MainWindow;

            var dialogResult = await window.ShowMessageAsync("Connecting Failed", message,
                                                                     MessageDialogStyle.AffirmativeAndNegative, dialogSetting);
            if (dialogResult == MessageDialogResult.Negative || dialogResult == MessageDialogResult.Canceled)
            {
              break;
            }
        }
        else 
        {
          PART_DisconnectMenuItem.IsEnabled = true;
          config.InsertServer(result.Servername);
          config.Save();
        }
      }
      while (!ok);
      UpdateDatabaseList();
    }

    private void DisconnectButton_Click(object sender, RoutedEventArgs e)
    {
      var ctx = this.DataContext as MainPanelViewModel;
      ctx.RemoveDbContext();
      PART_DisconnectMenuItem.IsEnabled = false;
      UpdateDatabaseList();
    }

    private void DatabaseNameButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void UpdateDatabaseList()
    {
      Type type = DataContext.GetType();
      if (type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Any(x => x.Name == "Databases"))
      {
         PropertyInfo prop = type.GetProperty("Databases");
         object value = prop.GetValue(DataContext);
         var dbs = value as IEnumerable<string>;
         if (dbs != null)
         {
           UpdateToggleButton(
               dbs.Select(
                 x => { 
                 var mi = new MenuItem();
                   mi.Header = x;
                   mi.Name = x;
                   mi.Click += DatabaseNameButton_Click;
                   mi.VerticalAlignment = VerticalAlignment.Bottom;
                   return mi;
                 }));
         }
      }
    }

    private void ToggleButton_Loaded(object sender, RoutedEventArgs e)
    {
      var btn = (ToggleButton)sender;

      btn.SetBinding(ToggleButton.IsCheckedProperty, new Binding("IsOpen") { Source = btn.ContextMenu });
      btn.ContextMenu.PlacementTarget = btn;
      btn.ContextMenu.Placement = PlacementMode.Top;
      UpdateToggleButton(null);
    }

    private void UpdateToggleButton(IEnumerable<object> items)
    {
      PART_RightToggleButton.ContextMenu.Items.Clear();
      if (items != null && items.Count() > 0)
      {
        foreach(var i in items)
        {
          PART_RightToggleButton.ContextMenu.Items.Add(i);
        }
        PART_RightToggleButton.ContextMenu.Items.Add(new Separator());
      }
      PART_RightToggleButton.ContextMenu.Items.Add(PART_ConnectMenuItem);
      PART_RightToggleButton.ContextMenu.Items.Add(PART_DisconnectMenuItem);
    }
  }
}
