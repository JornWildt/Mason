using log4net;
using Mason.IssueTracker.Server.ResourceCommons.Codecs;
using Mason.IssueTracker.Server.ResourceCommons.Handlers;
using Mason.IssueTracker.Server.ResourceCommons.Resources;
using OpenRasta.Configuration;


namespace Mason.IssueTracker.Server.ResourceCommons
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting ResourceCommon");
      ResourceSpace.Has.ResourcesOfType<ResourceCommonResource>()
        .AtUri("/resource-common")
        .HandledBy<ResourceCommonHandler>()
        .TranscodedBy<ResourceCommonCodec>();
    }
  }
}
