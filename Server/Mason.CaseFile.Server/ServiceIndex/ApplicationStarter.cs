using log4net;
using Mason.CaseFile.Server.CaseFiles.Codecs;
using Mason.CaseFile.Server.CaseFiles.Handlers;
using Mason.CaseFile.Server.ServiceIndex.Codecs;
using Mason.CaseFile.Server.ServiceIndex.Handlers;
using OpenRasta.Configuration;


namespace Mason.CaseFile.Server.ServiceIndex
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting ServiceIndex");
      ResourceSpace.Has.ResourcesOfType<Resources.ServiceIndexResource>()
        .AtUri("/service-index")
        .HandledBy<ServiceIndexHandler>()
        .TranscodedBy<ServiceIndexCodec>();
    }
  }
}
