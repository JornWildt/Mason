using log4net;
using OpenRasta.Configuration;
using OpenRasta.Web.UriDecorators;


namespace Mason.CaseFile.Server
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
        CaseFiles.ApplicationStarter.Start();
        //Origin.ApplicationStarter.Start();
      }
    }

    
    private void InitializeLogging()
    {
      log4net.Config.XmlConfigurator.Configure();
      Logger.Info("***********************************************************************");
      Logger.Info("Starting CaseFile server");
      Logger.Info("***********************************************************************");
    }
  }
}
