using log4net;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Domain.NHibernate.Attachments;
using Mason.IssueTracker.Server.Attachments.Codecs;
using Mason.IssueTracker.Server.Attachments.Handlers;
using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.IssueTracker.Codecs;
using OpenRasta.Configuration;
using OpenRasta.DI;


namespace Mason.IssueTracker.Server.Attachments
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Attachments");
      ResourceSpace.Uses.CustomDependency<IAttachmentRepository, AttachmentRepository>(DependencyLifetime.Singleton);

      ResourceSpace.Has.ResourcesOfType<AttachmentResource>()
        .AtUri(UrlPaths.Attachment)
        .HandledBy<AttachmentHandler>()
        .TranscodedBy<AttachmentCodec>();

      ResourceSpace.Has.ResourcesOfType<AttachmentContentResource>()
        .AtUri(UrlPaths.AttachmentContent)
        .HandledBy<AttachmentContentHandler>();

      ResourceSpace.Has.ResourcesOfType<UpdateAttachmentArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<UpdateAttachmentArgs>>();

      ResourceSpace.Uses.CustomDependency<JsonReader<UpdateAttachmentArgs>, JsonReader<UpdateAttachmentArgs>>(DependencyLifetime.Transient);
    }
  }
}
