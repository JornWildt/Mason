using log4net;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.NHibernate.Issues;
using Mason.IssueTracker.Server.Issues.Codecs;
using Mason.IssueTracker.Server.Issues.Handlers;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.IssueTracker.Codecs;
using OpenRasta.Configuration;
using OpenRasta.DI;


namespace Mason.IssueTracker.Server.Issues
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Issues");
      ResourceSpace.Uses.CustomDependency<IIssueRepository, IssueRepository>(DependencyLifetime.Singleton);

      ResourceSpace.Has.ResourcesOfType<IssueResource>()
        .AtUri(UrlPaths.Issue)
        .HandledBy<IssueHandler>()
        .TranscodedBy<IssueCodec>();

      ResourceSpace.Has.ResourcesOfType<IssueQueryResource>()
        .AtUri(UrlPaths.IssueQuery)
        .HandledBy<IssueQueryHandler>()
        .TranscodedBy<IssueQueryCodec>();

      ResourceSpace.Has.ResourcesOfType<IssueAttachmentsResource>()
        .AtUri(UrlPaths.IssueAttachments)
        .HandledBy<IssueAttachmentsHandler>();

      // This is anoying - is there are better way?

      ResourceSpace.Has.ResourcesOfType<CreateIssueArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<CreateIssueArgs>>();

      ResourceSpace.Has.ResourcesOfType<UpdateIssueArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<UpdateIssueArgs>>();

      ResourceSpace.Has.ResourcesOfType<AddAttachmentArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<AddAttachmentArgs>>();

      ResourceSpace.Uses.CustomDependency<JsonReader<CreateIssueArgs>, JsonReader<CreateIssueArgs>>(DependencyLifetime.Transient);
      //ResourceSpace.Uses.CustomDependency<JsonReader<UpdateIssueArgs>, JsonReader<UpdateIssueArgs>>(DependencyLifetime.Transient);
    }
  }
}
