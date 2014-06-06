using ApiExplorer.ViewModels;
using System.Windows;


namespace ApiExplorer.Windows
{
  /// <summary>
  /// Interaction logic for BasicAuthorizationSetupWindow.xaml
  /// </summary>
  public partial class BasicAuthorizationSetupWindow : Window
  {
    public BasicAuthorizationSetupWindow(BasicAuthorizationSetupViewModel vm)
    {
      InitializeComponent();
      vm.OwnerWindow = this;
      DataContext = vm;

      UsernameInput.Focus();
    }

    
    internal static bool ShowBasicAuthorizationSetup(string prompt, out string username, out string password)
    {
      BasicAuthorizationSetupViewModel vm = new BasicAuthorizationSetupViewModel(null, prompt);
      BasicAuthorizationSetupWindow w = new BasicAuthorizationSetupWindow(vm);

      w.ShowDialog();

      if (vm.Success)
      {
        username = vm.Username;
        password = vm.Password;
        return true;
      }
      else
      {
        username = null;
        password = null;
        return false;
      }
    }
  }
}
