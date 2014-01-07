using log4net;
using Mason.IssueTracker.Server.Issues.Codecs;
using Mason.IssueTracker.Server.Issues.Handlers;
using Mason.IssueTracker.Server.Origin.Codecs;
using Mason.IssueTracker.Server.Origin.Handlers;
using OpenRasta.Configuration;


namespace Mason.IssueTracker.Server.Origin
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Origin");
      ResourceSpace.Has.ResourcesOfType<Resources.OriginResource>()
        .AtUri("/origin")
        .HandledBy<OriginHandler>()
        .TranscodedBy<OriginCodec>();

      ResourceSpace.Has.ResourcesOfType<Resources.OriginContactResource>()
        .AtUri("/origin-contact")
        .HandledBy<OriginContactHandler>()
        .TranscodedBy<OriginContactCodec_jCard>()
        .And.TranscodedBy<OriginContactCodec_vCard>()
        .And.TranscodedBy<OriginContactCodec_Mason>();
    }
  }
}
