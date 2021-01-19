using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using Prism.Unity;
using Unity;
using Prism.Services.Dialogs;
using MahApps.Metro.Controls.Dialogs;

namespace SQLServerTools.ViewModels
{
  public abstract class ViewModelBase : BindableBase
  {
    /// <summary>
    /// DIコンテナを取得します
    /// コンストラクタ実行後に登録するため、コンストラクタ内で設定する場合は別途コンストラクタに引数を受け取って処理してください
    /// </summary>
    [Dependency]
    public IUnityContainer Container { get; set; }

    /// <summary>
    /// DIコンテナからEventAggregatorを登録します
    /// コンストラクタ実行後に登録するため、コンストラクタ内で設定する場合は別途コンストラクタに引数を受け取って処理してください
    /// </summary>
    [Dependency]
    public IEventAggregator EventAggregator { get; set; }

    /// <summary>
    /// DIコンテナからRegionManagerを登録します
    /// コンストラクタ実行後に登録するため、コンストラクタ内で設定する場合は別途コンストラクタに引数を受け取って処理してください
    /// </summary>s
    [Dependency]
    public IRegionManager RegionManager { get; set; }

    /// <summary>
    /// DIコンテナからDialogServiceを登録します
    /// コンストラクタ実行後に登録するため、コンストラクタ内で設定する場合は別途コンストラクタに引数を受け取って処理してください
    /// </summary>
    [Dependency]
    public IDialogService DialogService { get; set; }

    /// <summary>
    /// DIコンテナからDialogServiceを登録します
    /// コンストラクタ実行後に登録するため、コンストラクタ内で設定する場合は別途コンストラクタに引数を受け取って処理してください
    /// </summary>
    [Dependency]
    public IDialogCoordinator DialogCoordinator { get; set; }

    protected ViewModelBase() 
    { 
      this.Initialize();
      var container = Prism.Ioc.ContainerLocator.Container.GetContainer();
      this.Initialize(container);
      this.InitializeEvent(container.Resolve<IEventAggregator>());
      this.InitializeRegion(container.Resolve<IRegionManager>());
      this.InitializeDialog(container.Resolve<IDialogService>());
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public virtual void Initialize() { }

    public virtual void Initialize(IUnityContainer unityContainer) { }

    public virtual void InitializeEvent(IEventAggregator eventAggregator) { }

    public virtual void InitializeRegion(IRegionManager regionManager) { }

    public virtual void InitializeDialog(IDialogService dialogService) { }
  }
}
