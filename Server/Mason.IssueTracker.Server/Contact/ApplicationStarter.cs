using log4net;
using Mason.IssueTracker.Server.Contact.Codecs;
using Mason.IssueTracker.Server.Contact.Handlers;
using Mason.IssueTracker.Server.Contact.Resources;
using OpenRasta.Configuration;


namespace Mason.IssueTracker.Server.Contact
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Contact");

      ResourceSpace.Has.ResourcesOfType<ContactResource>()
        .AtUri("/contact")
        .HandledBy<ContactHandler>()
        .TranscodedBy<ContactCodec_jCard>()
        .And.TranscodedBy<ContactCodec_vCard>()
        .And.TranscodedBy<ContactCodec>();
    }
  }
}
