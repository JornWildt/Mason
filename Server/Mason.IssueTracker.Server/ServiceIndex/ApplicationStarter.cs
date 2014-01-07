using log4net;
using Mason.IssueTracker.Server.Issues.Codecs;
using Mason.IssueTracker.Server.Issues.Handlers;
using Mason.IssueTracker.Server.ServiceIndex.Codecs;
using Mason.IssueTracker.Server.ServiceIndex.Handlers;
using OpenRasta.Configuration;


namespace Mason.IssueTracker.Server.ServiceIndex
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
