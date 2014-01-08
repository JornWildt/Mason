using log4net;
using OpenRasta.Configuration;
using OpenRasta.Web.UriDecorators;


namespace Mason.IssueTracker.Server
{
  public class Configuration : IConfigurationSource
  {
    static ILog Logger = LogManager.GetLogger(typeof(Configuration));


    public void Configure()
    {
      InitializeLogging();

      using (OpenRastaConfiguration.Manual)
      {
        ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
        Issues.ApplicationStarter.Start();
        Contact.ApplicationStarter.Start();
        ResourceCommons.ApplicationStarter.Start();
        ServiceIndex.ApplicationStarter.Start();
      }
    }

    
    private void InitializeLogging()
    {
      log4net.Config.XmlConfigurator.Configure();
      Logger.Info("***********************************************************************");
      Logger.Info("Starting IssueTracker server");
      Logger.Info("***********************************************************************");
    }
  }
}
