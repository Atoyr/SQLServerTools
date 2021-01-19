using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
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
  public partial class BasePanelViewModel : ViewModelBase
  {
    public ReactiveProperty<string> LeftStatusMessage { get; } = new ReactiveProperty<string>();
    public ReactiveProperty<string> CenterStatusMessage { get; } = new ReactiveProperty<string>();
    public ReactiveProperty<string> RightStatusMessage { get; } = new ReactiveProperty<string>();

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
    }

    public bool UpdateDbContext(string server, bool integratedSecurity, string username, string password)
    {
      var builder = new SqlConnectionStringBuilder(); 
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
      }
      catch (Exception e)
      {
        this.LeftStatusMessage.Value = e.Message;
        return false;
      }




      return false;
    }

    public bool RemoveDbContext()
    {
      return false;
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
  }
}
