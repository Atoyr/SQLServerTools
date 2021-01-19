using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SQLServerTools.Views.Dialogs
{
  public class DbLoginDialogSettings : MetroDialogSettings
  {

    private const string DefaultServernameWatermark = "Servername...";
    private const string DefaultUsernameWatermark = "Username...";
    private const string DefaultPasswordWatermark = "Password...";
    private const string DefaultIntegratedSecurityCheckBoxText  = "Use Windows Account";
    private const string DefaultRememberCheckBoxText = "Remember";

    public DbLoginDialogSettings()
    {
      this.ServernameWatermark = DefaultServernameWatermark;
      this.UsernameWatermark = DefaultUsernameWatermark;
      this.UsernameCharacterCasing = CharacterCasing.Normal;
      this.PasswordWatermark = DefaultPasswordWatermark;
      this.NegativeButtonVisibility = Visibility.Collapsed;
      this.ShouldHideServername = false;
      this.ShouldHideUsername = false;
      this.AffirmativeButtonText = "Login";
      this.EnablePasswordPreview = false;
      this.IntegratedSecurityCheckBoxVisibility = Visibility.Collapsed;
      this.IntegratedSecurityCheckBoxText = DefaultIntegratedSecurityCheckBoxText;
      this.IntegratedSecurityCheckBoxChecked = false;
      this.RememberCheckBoxVisibility = Visibility.Collapsed;
      this.RememberCheckBoxText = DefaultRememberCheckBoxText;
      this.RememberCheckBoxChecked = false;
    }

    public string InitialServername { get; set; }

    public string InitialUsername { get; set; }

    public string InitialPassword { get; set; }

    public string ServernameWatermark { get; set; }

    public string UsernameWatermark { get; set; }

    public CharacterCasing UsernameCharacterCasing { get; set; }

    public bool ShouldHideServername { get; set; }

    public bool ShouldHideUsername { get; set; }

    public string PasswordWatermark { get; set; }

    public Visibility NegativeButtonVisibility { get; set; }

    public bool EnablePasswordPreview { get; set; }

    public Visibility IntegratedSecurityCheckBoxVisibility { get; set; }

    public string IntegratedSecurityCheckBoxText { get; set; }

    public bool IntegratedSecurityCheckBoxChecked { get; set; }

    public Visibility RememberCheckBoxVisibility { get; set; }

    public string RememberCheckBoxText { get; set; }

    public bool RememberCheckBoxChecked { get; set; }
  }
}
