using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ApiExplorer.MediaTypeHandlers.Image.ViewModels;
using ApiExplorer.ViewModels;
using log4net;


namespace ApiExplorer.MediaTypeHandlers.Image.UserControls
{
  public partial class ImageRender : UserControl, IDisposable
  {
    private static ILog Logger = LogManager.GetLogger(typeof(ImageRender));

    public ImageRender(ImageViewModel vm)
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

      
      if (DataContext is ImageViewModel)
      {
        try
        {
          File.Delete(((ImageViewModel)DataContext).Filename);
        }
        catch (Exception ex)
        {
          string msg = string.Format("Failed to delete temporary file '{0}' ({1})", ((ImageViewModel)DataContext).Filename, ex.Message);
          MessageBox.Show(Window.GetWindow(this), msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          Logger.Warn(msg, ex);
        }
      }
    }

    #endregion
  }
}
