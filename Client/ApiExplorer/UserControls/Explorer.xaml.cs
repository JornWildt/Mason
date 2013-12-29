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

      UrlInput.Focus();
    }
  }
}
