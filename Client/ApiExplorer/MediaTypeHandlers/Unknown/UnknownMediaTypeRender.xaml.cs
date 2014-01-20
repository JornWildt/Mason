using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Unknown
{
  /// <summary>
  /// Interaction logic for UnknownMediaType.xaml
  /// </summary>
  public partial class UnknownMediaTypeRender : UserControl
  {
    public UnknownMediaTypeRender(string content)
    {
      InitializeComponent();
      ContentElement.Text = content;
    }
  }
}
