using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.Windows
{
  public partial class MultipartJsonActionPopupWindow : Window
  {
    public MultipartJsonActionPopupWindow(MultipartJsonActionViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
