using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  /// <summary>
  /// Interaction logic for ApplicationMasonRender.xaml
  /// </summary>
  public partial class ApplicationMasonRender : UserControl
  {
    public ApplicationMasonRender(MasonViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
