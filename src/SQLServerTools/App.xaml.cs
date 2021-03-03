using System;
using System.Windows;
using System.Data.Common;
using System.Data.SqlClient;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using Prism.Regions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Theming;
using ControlzEx.Theming;
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

      // Add custom theme resource dictionaries
      // You should replace SampleApp with your application name
      // and the correct place where your custom theme lives
      var theme = ThemeManager.Current.AddLibraryTheme(
          new LibraryTheme(
            new Uri("pack://application:,,,/SQLServerTools;component/Styles/Themes/Dark.Default.xaml"),
            MahAppsLibraryThemeProvider.DefaultInstance
            )
          );

      ThemeManager.Current.ChangeTheme(this, theme);
    }
  }
}
