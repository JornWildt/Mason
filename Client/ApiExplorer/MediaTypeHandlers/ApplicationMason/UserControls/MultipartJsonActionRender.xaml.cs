using System.Windows.Controls;
using System.Windows.Input;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.UserControls
{
  /// <summary>
  /// Interaction logic for MultipartJsonActionRender.xaml
  /// </summary>
  public partial class MultipartJsonActionRender : UserControl
  {
    public MultipartJsonActionRender()
    {
      InitializeComponent();
      Keyboard.Focus(JsonInput);
    }
  }
}
