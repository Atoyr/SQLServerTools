using System.IO;
using Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Ioc;
using SQLServerTools.ViewModels;
using SQLServerTools.Config;

namespace SQLServerTools
{
  class Module : IModule
  {
    [Dependency]
    public IUnityContainer Container { get; set; }

    [Dependency]
    public IRegionManager RegionManager { get; set; }

    public void Initialize()
    {
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
      //throw new NotImplementedException();
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.Register<MainPanelViewModel>();
      containerRegistry.Register<MenuViewModel>();
      containerRegistry.RegisterInstance<Config.DbConnection>(Config.DbConnection.Load());
    }
  }
}


