using ApiExplorer.MediaTypeHandlers.Text.ViewModels;
using ApiExplorer.ViewModels;
using System;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Text.UserControls
{
  /// <summary>
  /// Interaction logic for TextRender.xaml
  /// </summary>
  public partial class TextRender : UserControl, IDisposable
  {
    public TextRender(TextViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }


    #region IDisposable Members

    public void Dispose()
    {
      if (DataContext is ViewModel)
      {
        ((ViewModel)DataContext).RemoveFromParent();
      }
    }

    #endregion
  }
}
