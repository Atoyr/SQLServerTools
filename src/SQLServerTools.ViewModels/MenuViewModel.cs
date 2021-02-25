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
using System.Reactive.Subjects;
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
    public ReactiveCommand AddTablePanel { get; private set; }
    public ReactiveCommand AddStatsPanel { get; private set; }
    private Subject<bool> AddTablePanelSource { get; set; }
    private Subject<bool> AddStatsPanelSource { get; set; }

    public override void Initialize()
    {
      AddTablePanelSource = new Subject<bool>();
      AddTablePanel = AddTablePanelSource.ToReactiveCommand(false);
      AddStatsPanelSource = new Subject<bool>();
      AddStatsPanel = AddStatsPanelSource.ToReactiveCommand(false);
    }

    public override void InitializeEvent(IEventAggregator eventAggregator)
    {
      AddTablePanel.Subscribe(() => eventAggregator.GetEvent<AddPanel>().Publish("Table"));
      eventAggregator.GetEvent<ChangedHasDbConnection>().Subscribe(AddTablePanelSource.OnNext);
      AddStatsPanel.Subscribe(() => eventAggregator.GetEvent<AddPanel>().Publish("Stats"));
      eventAggregator.GetEvent<ChangedHasDbConnection>().Subscribe(AddStatsPanelSource.OnNext);
    }
  }
}
