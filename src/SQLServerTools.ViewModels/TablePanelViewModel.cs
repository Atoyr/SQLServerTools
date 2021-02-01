using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using Prism.Unity;
using Prism.Commands;
using Unity;
using Reactive.Bindings;
using SQLServerTools.Messenger.Events;
using SQLServerTools.Database;
using SQLServerTools.ViewModels.DataModels;

namespace SQLServerTools.ViewModels
{
  public class TablePanelViewModel : ViewModelBase
  {
    private const string DefaultDatabase = "master";
    public ObservableCollection<DataModels.Table> Tables { get; } = new ObservableCollection<DataModels.Table>();
    public ObservableCollection<string> DatabaseList { get; } = new ObservableCollection<string>();
    public ReactiveProperty<string> TargetDatabase { get; } = new ReactiveProperty<string>();

    public override void Initialize(IUnityContainer container)
    {
      this.Container = container;
      var builder = container.Resolve<DbConnectionStringBuilder>();
      if (string.IsNullOrEmpty(builder.ConnectionString))
      {

      }

      DatabaseList.AddRange(Database.Database.GetDatabases(builder.ConnectionString).Select(x => x.Name));
      TargetDatabase.Value = DefaultDatabase;
      FetchTables();
    }

    public (bool ok, string message) FetchTables()
    {
      var builder = Container.Resolve<DbConnectionStringBuilder>();
      if (string.IsNullOrEmpty(builder.ConnectionString))
      {
        return (false, "ConnectionString not found");
      }
      Tables.Clear();
      try
      {
        Tables.AddRange(Database.Table.GetTables(builder.ConnectionString, TargetDatabase.Value).Select(x => DataModels.Table.ConvertTable(x)));
      }
      catch (Exception e)
      {
        return (false, e.Message);
      }
      return (true, string.Empty);
    }
  }
}
