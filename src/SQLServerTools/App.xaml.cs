using System.Windows;
using System.Data.Common;
using System.Data.SqlClient;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using Prism.Regions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Unity;

namespace SQLServerTools
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry) 
    {
      containerRegistry.Register<DbConnectionStringBuilder,SqlConnectionStringBuilder>();
      containerRegistry.RegisterInstance<IDialogCoordinator>(DialogCoordinator.Instance);
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
      base.ConfigureModuleCatalog(moduleCatalog);
      moduleCatalog.AddModule(typeof(Module));
    }

    protected override IModuleCatalog CreateModuleCatalog()
    {
      return new ConfigurationModuleCatalog();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
    }
  }
}
