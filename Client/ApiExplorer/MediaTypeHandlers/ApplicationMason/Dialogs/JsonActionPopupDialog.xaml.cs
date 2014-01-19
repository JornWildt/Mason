using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs
{
  public partial class JsonActionPopupDialog : Window
  {
    public JsonActionPopupDialog(JsonActionViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
