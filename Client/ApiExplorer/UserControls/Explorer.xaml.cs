using System.Windows.Controls;


namespace ApiExplorer.UserControls
{
  /// <summary>
  /// Interaction logic for Explorer.xaml
  /// </summary>
  public partial class Explorer : UserControl
  {
    public Explorer()
    {
      InitializeComponent();
    }

    
    private void UrlInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
    {
      UrlInput.SelectAll();
    }

    
    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      UrlInput.Focus();
      UrlInput.SelectAll();
    }
  }
}
