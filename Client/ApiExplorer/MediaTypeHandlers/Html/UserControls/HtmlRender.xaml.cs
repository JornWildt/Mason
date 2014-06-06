using ApiExplorer.MediaTypeHandlers.Html.ViewModels;
using ApiExplorer.ViewModels;
using log4net;
using System;
using System.Windows;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Html.UserControls
{
  /// <summary>
  /// Interaction logic for HtmlRender.xaml
  /// </summary>
  public partial class HtmlRender : UserControl, IDisposable
  {
    private static ILog Logger = LogManager.GetLogger(typeof(HtmlRender));

    public HtmlRender(HtmlViewModel vm, Uri baseUri)
    {
      InitializeComponent();
      DataContext = vm;
      Browser.LoadCompleted += Browser_LoadCompleted;
    }


    void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
      // How to set base URL of document? Would be nice for relative URLs in for instance stylesheets
    }


    #region IDisposable Members

    public void Dispose()
    {
      if (DataContext is ViewModel)
      {
        ((ViewModel)DataContext).RemoveFromParent();
      }


      if (DataContext is HtmlViewModel)
      {
        try
        {
          System.IO.File.Delete(((HtmlViewModel)DataContext).Source);
        }
        catch (Exception ex)
        {
          string msg = string.Format("Failed to delete temporary file '{0}' ({1})", ((HtmlViewModel)DataContext).Source, ex.Message);
          MessageBox.Show(Window.GetWindow(this), msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          Logger.Warn(msg, ex);
        }
      }
    }

    #endregion
  }
}
