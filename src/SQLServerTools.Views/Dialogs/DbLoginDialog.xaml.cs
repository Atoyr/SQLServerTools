using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.ValueBoxes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SQLServerTools.Views.Dialogs
{
  public partial class DbLoginDialog : BaseMetroDialog
  {
    private CancellationTokenRegistration cancellationTokenRegistration;

    /// <summary>Identifies the <see cref="Message"/> dependency property.</summary>
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string Message
    {
      get { return (string)this.GetValue(MessageProperty); }
      set { this.SetValue(MessageProperty, value); }
    }

    /// <summary>Identifies the <see cref="Servername"/> dependency property.</summary>
    public static readonly DependencyProperty ServernameProperty = DependencyProperty.Register(nameof(Servername), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string Servername
    {
      get { return (string)this.GetValue(ServernameProperty); }
      set { this.SetValue(ServernameProperty, value); }
    }

    /// <summary>Identifies the <see cref="Username"/> dependency property.</summary>
    public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register(nameof(Username), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string Username
    {
      get { return (string)this.GetValue(UsernameProperty); }
      set { this.SetValue(UsernameProperty, value); }
    }

    /// <summary>Identifies the <see cref="ServernameWatermark"/> dependency property.</summary>
    public static readonly DependencyProperty ServernameWatermarkProperty = DependencyProperty.Register(nameof(ServernameWatermark), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string ServernameWatermark
    {
      get { return (string)this.GetValue(ServernameWatermarkProperty); }
      set { this.SetValue(ServernameWatermarkProperty, value); }
    }

    /// <summary>Identifies the <see cref="UsernameWatermark"/> dependency property.</summary>
    public static readonly DependencyProperty UsernameWatermarkProperty = DependencyProperty.Register(nameof(UsernameWatermark), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string UsernameWatermark
    {
      get { return (string)this.GetValue(UsernameWatermarkProperty); }
      set { this.SetValue(UsernameWatermarkProperty, value); }
    }

    /// <summary>Identifies the <see cref="UsernameCharacterCasing"/> dependency property.</summary>
    public static readonly DependencyProperty UsernameCharacterCasingProperty = DependencyProperty.Register(nameof(UsernameCharacterCasing), typeof(CharacterCasing), typeof(DbLoginDialog), new PropertyMetadata(default(CharacterCasing)));

    public CharacterCasing UsernameCharacterCasing
    {
      get { return (CharacterCasing)this.GetValue(UsernameCharacterCasingProperty); }
      set { this.SetValue(UsernameCharacterCasingProperty, value); }
    }

    /// <summary>Identifies the <see cref="Password"/> dependency property.</summary>
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(nameof(Password), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string Password
    {
      get { return (string)this.GetValue(PasswordProperty); }
      set { this.SetValue(PasswordProperty, value); }
    }

    /// <summary>Identifies the <see cref="PasswordWatermark"/> dependency property.</summary>
    public static readonly DependencyProperty PasswordWatermarkProperty = DependencyProperty.Register(nameof(PasswordWatermark), typeof(string), typeof(DbLoginDialog), new PropertyMetadata(default(string)));

    public string PasswordWatermark
    {
      get { return (string)this.GetValue(PasswordWatermarkProperty); }
      set { this.SetValue(PasswordWatermarkProperty, value); }
    }

    /// <summary>Identifies the <see cref="AffirmativeButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty AffirmativeButtonTextProperty = DependencyProperty.Register(nameof(AffirmativeButtonText), typeof(string), typeof(DbLoginDialog), new PropertyMetadata("OK"));

    public string AffirmativeButtonText
    {
      get { return (string)this.GetValue(AffirmativeButtonTextProperty); }
      set { this.SetValue(AffirmativeButtonTextProperty, value); }
    }

    /// <summary>Identifies the <see cref="NegativeButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register(nameof(NegativeButtonText), typeof(string), typeof(DbLoginDialog), new PropertyMetadata("Cancel"));

    public string NegativeButtonText
    {
      get { return (string)this.GetValue(NegativeButtonTextProperty); }
      set { this.SetValue(NegativeButtonTextProperty, value); }
    }

    /// <summary>Identifies the <see cref="NegativeButtonButtonVisibility"/> dependency property.</summary>
    public static readonly DependencyProperty NegativeButtonButtonVisibilityProperty = DependencyProperty.Register(nameof(NegativeButtonButtonVisibility), typeof(Visibility), typeof(DbLoginDialog), new PropertyMetadata(Visibility.Collapsed));

    public Visibility NegativeButtonButtonVisibility
    {
      get { return (Visibility)this.GetValue(NegativeButtonButtonVisibilityProperty); }
      set { this.SetValue(NegativeButtonButtonVisibilityProperty, value); }
    }

    /// <summary>Identifies the <see cref="ShouldHideServername"/> dependency property.</summary>
    public static readonly DependencyProperty ShouldHideServernameProperty = DependencyProperty.Register(nameof(ShouldHideServername), typeof(bool), typeof(DbLoginDialog), new PropertyMetadata(BooleanBoxes.FalseBox));

    public bool ShouldHideServername
    {
      get { return (bool)this.GetValue(ShouldHideServernameProperty); }
      set { this.SetValue(ShouldHideServernameProperty, BooleanBoxes.Box(value)); }
    }

    /// <summary>Identifies the <see cref="ShouldHideUsername"/> dependency property.</summary>
    public static readonly DependencyProperty ShouldHideUsernameProperty = DependencyProperty.Register(nameof(ShouldHideUsername), typeof(bool), typeof(DbLoginDialog), new PropertyMetadata(BooleanBoxes.FalseBox));

    public bool ShouldHideUsername
    {
      get { return (bool)this.GetValue(ShouldHideUsernameProperty); }
      set { this.SetValue(ShouldHideUsernameProperty, BooleanBoxes.Box(value)); }
    }

    /// <summary>Identifies the <see cref="IntegratedSecurityCheckBoxVisibility"/> dependency property.</summary>
    public static readonly DependencyProperty IntegratedSecurityCheckBoxVisibilityProperty = DependencyProperty.Register(nameof(IntegratedSecurityCheckBoxVisibility), typeof(Visibility), typeof(DbLoginDialog), new PropertyMetadata(Visibility.Collapsed));

    public Visibility IntegratedSecurityCheckBoxVisibility
    {
      get { return (Visibility)this.GetValue(IntegratedSecurityCheckBoxVisibilityProperty); }
      set { this.SetValue(IntegratedSecurityCheckBoxVisibilityProperty, value); }
    }

    /// <summary>Identifies the <see cref="IntegratedSecurityCheckBoxText"/> dependency property.</summary>
    public static readonly DependencyProperty IntegratedSecurityCheckBoxTextProperty = DependencyProperty.Register(nameof(IntegratedSecurityCheckBoxText), typeof(string), typeof(DbLoginDialog), new PropertyMetadata("IntegratedSecurity"));

    public string IntegratedSecurityCheckBoxText
    {
      get { return (string)this.GetValue(IntegratedSecurityCheckBoxTextProperty); }
      set { this.SetValue(IntegratedSecurityCheckBoxTextProperty, value); }
    }

    /// <summary>Identifies the <see cref="IntegratedSecurityCheckBoxChecked"/> dependency property.</summary>
    public static readonly DependencyProperty IntegratedSecurityCheckBoxCheckedProperty = DependencyProperty.Register(nameof(IntegratedSecurityCheckBoxChecked), typeof(bool), typeof(DbLoginDialog), new PropertyMetadata(BooleanBoxes.FalseBox));

    public bool IntegratedSecurityCheckBoxChecked
    {
      get { return (bool)this.GetValue(IntegratedSecurityCheckBoxCheckedProperty); }
      set { this.SetValue(IntegratedSecurityCheckBoxCheckedProperty, BooleanBoxes.Box(value)); }
    }

    /// <summary>Identifies the <see cref="RememberCheckBoxVisibility"/> dependency property.</summary>
    public static readonly DependencyProperty RememberCheckBoxVisibilityProperty = DependencyProperty.Register(nameof(RememberCheckBoxVisibility), typeof(Visibility), typeof(DbLoginDialog), new PropertyMetadata(Visibility.Collapsed));

    public Visibility RememberCheckBoxVisibility
    {
      get { return (Visibility)this.GetValue(RememberCheckBoxVisibilityProperty); }
      set { this.SetValue(RememberCheckBoxVisibilityProperty, value); }
    }

    /// <summary>Identifies the <see cref="RememberCheckBoxText"/> dependency property.</summary>
    public static readonly DependencyProperty RememberCheckBoxTextProperty = DependencyProperty.Register(nameof(RememberCheckBoxText), typeof(string), typeof(DbLoginDialog), new PropertyMetadata("Remember"));

    public string RememberCheckBoxText
    {
      get { return (string)this.GetValue(RememberCheckBoxTextProperty); }
      set { this.SetValue(RememberCheckBoxTextProperty, value); }
    }

    /// <summary>Identifies the <see cref="RememberCheckBoxChecked"/> dependency property.</summary>
    public static readonly DependencyProperty RememberCheckBoxCheckedProperty = DependencyProperty.Register(nameof(RememberCheckBoxChecked), typeof(bool), typeof(DbLoginDialog), new PropertyMetadata(BooleanBoxes.FalseBox));

    public bool RememberCheckBoxChecked
    {
      get { return (bool)this.GetValue(RememberCheckBoxCheckedProperty); }
      set { this.SetValue(RememberCheckBoxCheckedProperty, BooleanBoxes.Box(value)); }
    }

    internal SizeChangedEventHandler SizeChangedHandler { get; set; }

    public DbLoginDialog()
      : this(null)
    {
    }

    public DbLoginDialog(MetroWindow parentWindow)
      : this(parentWindow, null)
    {
    }

    public DbLoginDialog(MetroWindow parentWindow, DbLoginDialogSettings settings)
      : base(parentWindow, settings)
    {
      this.InitializeComponent();
      this.Servername = settings.InitialServername;
      this.Username = settings.InitialUsername;
      this.Password = settings.InitialPassword;
      this.UsernameCharacterCasing = settings.UsernameCharacterCasing;
      this.ServernameWatermark = settings.ServernameWatermark;
      this.UsernameWatermark = settings.UsernameWatermark;
      this.PasswordWatermark = settings.PasswordWatermark;
      this.NegativeButtonButtonVisibility = settings.NegativeButtonVisibility;
      this.ShouldHideServername = settings.ShouldHideServername;
      this.ShouldHideUsername = settings.ShouldHideUsername;
      this.IntegratedSecurityCheckBoxVisibility = settings.IntegratedSecurityCheckBoxVisibility;
      this.IntegratedSecurityCheckBoxText = settings.IntegratedSecurityCheckBoxText;
      this.IntegratedSecurityCheckBoxChecked = settings.IntegratedSecurityCheckBoxChecked;
      this.RememberCheckBoxVisibility = settings.RememberCheckBoxVisibility;
      this.RememberCheckBoxText = settings.RememberCheckBoxText;
      this.RememberCheckBoxChecked = settings.RememberCheckBoxChecked;
    }

    public Task<DbLoginDialogData> WaitForButtonPressAsync()
    {
      this.Dispatcher.BeginInvoke(new Action(() =>
            {
            this.Focus();
            if (string.IsNullOrEmpty(this.PART_TextBox.Text) && !this.ShouldHideServername)
            {
            this.PART_TextBox.Focus();
            }
            else if (string.IsNullOrEmpty(this.PART_TextBox2.Text) && !this.ShouldHideUsername)
            {
            this.PART_TextBox2.Focus();
            }
            else
            {
            this.PART_TextBox3.Focus();
            }
            }));

      TaskCompletionSource<DbLoginDialogData> tcs = new TaskCompletionSource<DbLoginDialogData>();

      RoutedEventHandler negativeHandler = null;
      KeyEventHandler negativeKeyHandler = null;

      RoutedEventHandler affirmativeHandler = null;
      KeyEventHandler affirmativeKeyHandler = null;

      KeyEventHandler escapeKeyHandler = null;

      Action cleanUpHandlers = () =>
      {
        this.PART_TextBox.KeyDown -= affirmativeKeyHandler;
        this.PART_TextBox2.KeyDown -= affirmativeKeyHandler;
        this.PART_TextBox3.KeyDown -= affirmativeKeyHandler;

        this.KeyDown -= escapeKeyHandler;

        this.PART_NegativeButton.Click -= negativeHandler;
        this.PART_AffirmativeButton.Click -= affirmativeHandler;

        this.PART_NegativeButton.KeyDown -= negativeKeyHandler;
        this.PART_AffirmativeButton.KeyDown -= affirmativeKeyHandler;

        this.cancellationTokenRegistration.Dispose();
      };

      this.cancellationTokenRegistration = this.DialogSettings
        .CancellationToken
        .Register(() =>
            {
            this.BeginInvoke(() =>
                {
                cleanUpHandlers();
                tcs.TrySetResult(null);
                });
            });

      escapeKeyHandler = (sender, e) =>
      {
        if (e.Key == Key.Escape || (e.Key == Key.System && e.SystemKey == Key.F4))
        {
          cleanUpHandlers();

          tcs.TrySetResult(null);
        }
      };

      negativeKeyHandler = (sender, e) =>
      {
        if (e.Key == Key.Enter)
        {
          cleanUpHandlers();

          tcs.TrySetResult(null);
        }
      };

      affirmativeKeyHandler = (sender, e) =>
      {
        if (e.Key == Key.Enter)
        {
          cleanUpHandlers();
          tcs.TrySetResult(new DbLoginDialogData
              {
              Servername = this.Servername,
              Username = this.Username,
              SecurePassword = this.PART_TextBox3.SecurePassword,
              IntegratedSecurity = this.IntegratedSecurityCheckBoxChecked,
              ShouldRemember = this.RememberCheckBoxChecked
              });
        }
      };

      negativeHandler = (sender, e) =>
      {
        cleanUpHandlers();

        tcs.TrySetResult(null);

        e.Handled = true;
      };

      affirmativeHandler = (sender, e) =>
      {
        cleanUpHandlers();

        tcs.TrySetResult(new DbLoginDialogData
            {
            Servername = this.Servername,
            Username = this.Username,
            SecurePassword = this.PART_TextBox3.SecurePassword,
            IntegratedSecurity = this.IntegratedSecurityCheckBoxChecked,
            ShouldRemember = this.RememberCheckBoxChecked
            });

        e.Handled = true;
      };

      this.PART_NegativeButton.KeyDown += negativeKeyHandler;
      this.PART_AffirmativeButton.KeyDown += affirmativeKeyHandler;

      this.PART_TextBox.KeyDown += affirmativeKeyHandler;
      this.PART_TextBox2.KeyDown += affirmativeKeyHandler;
      this.PART_TextBox3.KeyDown += affirmativeKeyHandler;

      this.KeyDown += escapeKeyHandler;

      this.PART_NegativeButton.Click += negativeHandler;
      this.PART_AffirmativeButton.Click += affirmativeHandler;

      this.PART_IntegratedSecurity.Checked += (sender, e) =>
      {
          this.PART_TextBox2.IsEnabled = false;
          if (string.IsNullOrEmpty(Environment.UserDomainName))
          {
            this.PART_TextBox2.Text = Environment.UserName;
          }
          else
          {
            this.PART_TextBox2.Text = $"{Environment.UserDomainName}\\{Environment.UserName}";
          }
          this.PART_TextBox3.IsEnabled = false;
          this.PART_TextBox3.Password = string.Empty;
      };
      this.PART_IntegratedSecurity.Unchecked += (sender, e) =>
      {
          this.PART_TextBox2.IsEnabled = true;
          this.PART_TextBox2.Text = string.Empty;
          this.PART_TextBox3.IsEnabled = true;
          this.PART_TextBox3.Password = string.Empty;
      };

      return tcs.Task;
    }

    protected override void OnLoaded()
    {
      var settings = this.DialogSettings as DbLoginDialogSettings;
      if (settings != null && settings.EnablePasswordPreview)
      {
        var win8MetroPasswordStyle = this.FindResource("MahApps.Styles.PasswordBox.Win8") as Style;
        if (win8MetroPasswordStyle != null)
        {
          this.PART_TextBox3.Style = win8MetroPasswordStyle;
          // apply template again to fire the loaded event which is necessary for revealed password
          this.PART_TextBox3.ApplyTemplate();
        }
      }

      this.AffirmativeButtonText = this.DialogSettings.AffirmativeButtonText;
      this.NegativeButtonText = this.DialogSettings.NegativeButtonText;

      switch (this.DialogSettings.ColorScheme)
      {
        case MetroDialogColorScheme.Accented:
          this.PART_NegativeButton.SetResourceReference(StyleProperty, "MahApps.Styles.Button.Dialogs.AccentHighlight");
          this.PART_TextBox.SetResourceReference(ForegroundProperty, "MahApps.Brushes.ThemeForeground");
          this.PART_TextBox2.SetResourceReference(ForegroundProperty, "MahApps.Brushes.ThemeForeground");
          this.PART_TextBox3.SetResourceReference(ForegroundProperty, "MahApps.Brushes.ThemeForeground");
          break;
      }
    }
    public new void OnClose()
    {
      // this is only set when a dialog is shown (externally) in it's OWN window.
      this.ParentDialogWindow?.Close();
    }
  }        
}
