using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using Prism.Unity;
using Unity;
using Prism.Services.Dialogs;
using Prism.Commands;
using SQLServerTools.ViewModels;
using Reactive.Bindings;

namespace SQLServerTools
{
  public class MainWindowViewModel : ViewModelBase
  {
    public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();

    [Dependency]
    public MainPanelViewModel MainPanelViewModel { get; set; }

    [Dependency]
    public MenuViewModel MenuViewModel { get; set; }

    public override void Initialize(IUnityContainer unityContainer)
    {
      this.Title.Value = "SQL Server Tools";
    }
  }
}


