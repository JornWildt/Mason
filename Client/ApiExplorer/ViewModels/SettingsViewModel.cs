using ApiExplorer.Utilities;
namespace ApiExplorer.ViewModels
{
  public class SettingsViewModel : ViewModel
  {
    public static SettingsViewModel Default = new SettingsViewModel();


    public bool PreferMinimalResponseSize
    {
      get { return SettingsReader.Get(() => ApiExplorer.Properties.Settings.Default.PreferMinimalResponseSize, false); }
      set
      {
        if (value != SettingsReader.Get(() => ApiExplorer.Properties.Settings.Default.PreferMinimalResponseSize, false))
        {
          SettingsReader.Set(() => ApiExplorer.Properties.Settings.Default.PreferMinimalResponseSize = value);
          OnPropertyChanged("PreferMinimalResponseSize");
        }
      }
    }


    public bool UseMethodOverride
    {
      get { return SettingsReader.Get(() => ApiExplorer.Properties.Settings.Default.UseMethodOverride, false); }
      set
      {
        if (value != SettingsReader.Get(() => ApiExplorer.Properties.Settings.Default.UseMethodOverride, false))
        {
          SettingsReader.Set(() => ApiExplorer.Properties.Settings.Default.UseMethodOverride = value);
          OnPropertyChanged("UseMethodOverride");
        }
      }
    }


    public SettingsViewModel()
      : base(null)
    {
    }
  }
}
