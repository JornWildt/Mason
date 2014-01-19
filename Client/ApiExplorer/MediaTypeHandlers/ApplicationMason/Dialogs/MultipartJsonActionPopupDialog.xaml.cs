using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs
{
  public partial class MultipartJsonActionPopupDialog : Window
  {
    public MultipartJsonActionPopupDialog(MultipartJsonActionViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
