using System.Windows.Controls;
using System.Windows.Input;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.UserControls
{
  /// <summary>
  /// Interaction logic for JsonActionRender.xaml
  /// </summary>
  public partial class JsonActionRender : UserControl
  {
    public JsonActionRender()
    {
      InitializeComponent();
      Keyboard.Focus(JsonInput);
    }
  }
}
