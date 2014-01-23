namespace ApiExplorer.ViewModels
{
  public class SettingsViewModel : ViewModel
  {
    public static SettingsViewModel Default = new SettingsViewModel();


    public bool PreferMinimalResponseSize
    {
      get { return ApiExplorer.Properties.Settings.Default.PreferMinimalResponseSize; }
      set
      {
        if (value != ApiExplorer.Properties.Settings.Default.PreferMinimalResponseSize)
        {
          ApiExplorer.Properties.Settings.Default.PreferMinimalResponseSize = value;
          OnPropertyChanged("PreferMinimalResponseSize");
        }
      }
    }


    public SettingsViewModel()
      : base(null)
    {
    }
  }
}
