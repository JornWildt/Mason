using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using System.Windows.Controls;
using System;
using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.UserControls
{
  /// <summary>
  /// Interaction logic for ApplicationMasonRender.xaml
  /// </summary>
  public partial class ApplicationMasonRender : UserControl, IDisposable
  {
    public ApplicationMasonRender(MasonViewModel vm)
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
