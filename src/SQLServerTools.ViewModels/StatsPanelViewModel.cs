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
  public class StatsPanelViewModel : ViewModelBase
  {
    public ObservableCollection<DataModels.Stats> Stats { get; } = new ObservableCollection<DataModels.Stats>();
    public ObservableCollection<string> DatabaseList { get; } = new ObservableCollection<string>();
    public ReactiveProperty<int> SelectIndex { get; } = new ReactiveProperty<int>();

    public override void Initialize(IUnityContainer container)
    {
      this.Container = container;
      var builder = container.Resolve<DbConnectionStringBuilder>();
      if (string.IsNullOrEmpty(builder.ConnectionString))
      {

      }

      DatabaseList.AddRange(Database.Database.GetDatabases(builder.ConnectionString).Select(x => x.Name));
      SelectIndex.Value = 0;
      FetchStats();
    }

    public (bool ok, string message) FetchStats()
    {
      var builder = Container.Resolve<DbConnectionStringBuilder>();
      if (string.IsNullOrEmpty(builder.ConnectionString))
      {
        return (false, "ConnectionString not found");
      }
      Stats.Clear();
      try
      {
        Stats.AddRange(Database.Stats.GetStats(builder.ConnectionString, DatabaseList[SelectIndex.Value]).Select(x => DataModels.Stats.ConvertStats(x)));
      }
      catch (Exception e)
      {
        return (false, e.Message);
      }
      return (true, string.Empty);
    }
  }
}
