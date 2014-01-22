using System.Windows;
using System.Windows.Threading;
using log4net;


namespace ApiExplorer
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    static ILog Logger = LogManager.GetLogger(typeof(App));

    protected override void OnStartup(StartupEventArgs e)
    {
      log4net.Config.XmlConfigurator.Configure();
      Logger.Info("******************************************************************");
      Logger.Info("Starting ApiExplorer");
      Logger.Info("******************************************************************");

      Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      Logger.Fatal("Unhandled exception", e.Exception);
    }
  }
}
