using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs
{
  /// <summary>
  /// Interaction logic for UrlTemplatePopup.xaml
  /// </summary>
  public partial class UrlTemplatePopupDialog : Window
  {
    public UrlTemplatePopupDialog(LinkTemplateViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
