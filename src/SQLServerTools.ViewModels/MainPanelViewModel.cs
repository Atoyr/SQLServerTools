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

namespace SQLServerTools.ViewModels
{
  public class MainPanelViewModel : ViewModelBase
  {
    public ReactiveProperty<string> LeftStatusMessage { get; } = new ReactiveProperty<string>();
    public ReactiveProperty<string> CenterStatusMessage { get; } = new ReactiveProperty<string>();
    public ReactiveProperty<string> RightStatusMessage { get; } = new ReactiveProperty<string>();
    public ObservableCollection<ViewModelBase> PanelViewModels { get; } = new ObservableCollection<ViewModelBase>();
    public ObservableCollection<string> Databases { get; } = new ObservableCollection<string>();

    // public ReactiveProperty<string> ServerName { get; } = new ReactiveProperty<string>();
    // public ReactiveProperty<bool> IntegratedSecurity { get; } = new ReactiveProperty<bool>();
    // public ReactiveProperty<string> LoginUser { get; } = new ReactiveProperty<string>();
    // public ReactiveProperty<string> LoginPassword { get; } = new ReactiveProperty<string>();

    public override void Initialize(IUnityContainer unityContainer)
    {
      RightStatusMessage.Value = "Disconnected";

    }

    public override void InitializeEvent(IEventAggregator eventAggregator)
    {
      eventAggregator.GetEvent<ChangeRequestLeftStatusMessageEvent>().Subscribe(ChangeLeftStatusMessage);
      eventAggregator.GetEvent<ChangeRequestCenterStatusMessageEvent>().Subscribe(ChangeCenterStatusMessage);
      eventAggregator.GetEvent<ChangeRequestRightStatusMessageEvent>().Subscribe(ChangeRightStatusMessage);
      eventAggregator.GetEvent<AddPanel>().Subscribe(AddPanel);
    }

    public (bool ok, string message) UpdateDbContext(string server, bool integratedSecurity, string username, string password)
    {
      var builder = new SqlConnectionStringBuilder(); 
      User user;
      try 
      {
        builder.DataSource = server;
        if ( integratedSecurity)
        {
          builder.IntegratedSecurity = true;
        }
        else
        {
          builder.IntegratedSecurity = false;
          builder.UserID = username;
          builder.Password = password;
        }

        Common.Ping(builder.ConnectionString);
        user = User.GetUser(builder.ConnectionString);
      }
      catch (Exception e)
      {
        return (false, e.Message);
      }

      Container.RegisterInstance<DbConnectionStringBuilder>(builder);
      ChangeRightStatusMessage($"{builder.DataSource} : {user.DefaultDbName} : {user.Name}");

      var (ok, message) = UpdateDbVersionMessage();
      if (!ok)
      {
        return (false, message);
      }
      (ok, message) = UpdateDatabases();
      if (!ok)
      {
        return (false, message);
      }
      EventAggregator.GetEvent<ChangedHasDbConnection>().Publish(true);

      return (true, string.Empty);
    }

    public bool RemoveDbContext()
    {
      var builder = Container.Resolve<DbConnectionStringBuilder>();
      builder.Clear();
      ChangeRightStatusMessage(RightStatusMessage.Value = "Disconnected");
      EventAggregator.GetEvent<ChangedHasDbConnection>().Publish(false);
      var (ok, _) = UpdateDbVersionMessage();
      if (!ok)
      {
        return false;
      }
      (ok, _) = UpdateDatabases();
      if (!ok)
      {
        return (false);
      }

      return true;
    }

    public (bool ok, string message) ChangeUseDatabase(string databaseName)
    {
      bool ok = false;
      string message = string.Empty;
      var builder = Container.Resolve<DbConnectionStringBuilder>();
      var connString = builder.ConnectionString;
      if (string.IsNullOrEmpty(connString))
      {
        ok = false;
        message = "DB Not Connecting";
      }
      else
      {
        try
        {
          var dbs = Database.Database.GetDatabases(connString);
          var result = dbs.Any(x => x.Name.ToUpper() == databaseName.ToUpper());
          if (!result)
          {
            ok = false;
            message = "database not found";
          }
          else 
          {
            if (builder is SqlConnectionStringBuilder b)
            {
              b.InitialCatalog = databaseName;
            }
          }
        }
        catch (Exception e)
        {
          ok = false;
          message = e.Message;
        }
      }

      return (ok, message);

    }

    private void ChangeLeftStatusMessage(string message)
    {
      LeftStatusMessage.Value = message;
    }

    private void ChangeCenterStatusMessage(string message)
    {
      CenterStatusMessage.Value = message;
    }

    private void ChangeRightStatusMessage(string message)
    {
      RightStatusMessage.Value = message;
    }

    private void AddPanel(string panelName)
    {
      ViewModelBase vm;
      switch(panelName)
      {
        case "Table":
          vm = new TablePanelViewModel();
          break;
        case "Stats":
          vm = new StatsPanelViewModel();
          break;
        default:
          return;
      }
      PanelViewModels.Add(vm);
    }

    private (bool ok, string message) UpdateDbVersionMessage()
    {
      bool ok = false;
      string message = string.Empty;
      var builder = Container.Resolve<DbConnectionStringBuilder>();
      var connString = builder.ConnectionString;
      if (string.IsNullOrEmpty(connString))
      {
        ChangeLeftStatusMessage(string.Empty);
        ok = true;
      }
      else
      {
        try
        {
          var serverProp = Database.ServerProperty.GetServerProperty(connString);
          ChangeLeftStatusMessage(serverProp.Version);
          ok = true;
        }
        catch (Exception e)
        {
          ok = false;
          message = e.Message;
        }
      }

      return (ok, message);
    }

    private (bool ok, string message) UpdateDatabases()
    {
      bool ok = false;
      string message = string.Empty;
      var builder = Container.Resolve<DbConnectionStringBuilder>();
      var connString = builder.ConnectionString;
      Databases.Clear();
      if (string.IsNullOrEmpty(connString))
      {
        ok = true;
      }
      else
      {
        try
        {
          var dbs = Database.Database.GetDatabases(connString);
          Databases.AddRange(dbs.Select(x => x.Name));
          ok = true;
        }
        catch (Exception e)
        {
          ok = false;
          message = e.Message;
        }
      }

      return (ok, message);

    }
  }
}
