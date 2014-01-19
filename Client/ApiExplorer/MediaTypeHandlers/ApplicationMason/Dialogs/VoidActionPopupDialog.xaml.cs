using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs
{
  public partial class VoidActionPopupDialog : Window
  {
    public VoidActionPopupDialog(VoidActionViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
