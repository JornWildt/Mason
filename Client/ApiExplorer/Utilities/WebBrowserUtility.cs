using System;
using System.Windows;
using System.Windows.Controls;


namespace ApiExplorer.Utilities
{
  public static class WebBrowserUtility
  {
    // Use this dependency property to bind with WPF WebBrowser property "Source" (which is not a dependency property itself)
    // <WebBrowser cwu:WebBrowserUtility.Source="{Binding ...}"/>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.RegisterAttached("Source", typeof(string), typeof(WebBrowserUtility), new PropertyMetadata(OnSourceChanged));


    public static string GetSource(DependencyObject dependencyObject)
    {
      return (string)dependencyObject.GetValue(SourceProperty);
    }


    public static void SetSource(DependencyObject dependencyObject, string Source)
    {
      dependencyObject.SetValue(SourceProperty, Source);
    }


    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var webBrowser = (WebBrowser)d;
      try
      {
        webBrowser.Source = new Uri((string)e.NewValue);
      }
      catch (Exception)
      {
        // Ignore
      }
    }
  }

}
