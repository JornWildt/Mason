using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Windows
{
  public partial class JsonActionPopupWindow : Window
  {
    public JsonActionPopupWindow(JsonActionViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
