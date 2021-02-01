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

namespace SQLServerTools.ViewModels
{
  public class MenuViewModel : ViewModelBase
  {
    public ReactiveCommand AddTablePanel { get; } = new ReactiveCommand();

    public override void InitializeEvent(IEventAggregator eventAggregator)
    {

      AddTablePanel.Subscribe(() => eventAggregator.GetEvent<AddPanel>().Publish("Table"));
    }
  }
}
