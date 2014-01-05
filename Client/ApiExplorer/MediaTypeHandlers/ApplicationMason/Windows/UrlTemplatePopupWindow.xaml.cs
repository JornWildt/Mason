using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Windows
{
  /// <summary>
  /// Interaction logic for UrlTemplatePopup.xaml
  /// </summary>
  public partial class UrlTemplatePopupWindow : Window
  {
    public UrlTemplatePopupWindow(LinkTemplateViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
