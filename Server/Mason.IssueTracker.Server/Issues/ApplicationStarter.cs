using log4net;
using Mason.IssueTracker.Server.Issues.Codecs;
using Mason.IssueTracker.Server.Issues.Handlers;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Utility;
using OpenRasta.Configuration;
using OpenRasta.DI;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.IssueTracker.Codecs;
using Mason.IssueTracker.Server.Domain.Comments;
using Mason.IssueTracker.Server.Codecs;


namespace Mason.IssueTracker.Server.Issues
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Issues");
      ResourceSpace.Uses.CustomDependency<IIssueRepository, IssueInMemoryRepository>(DependencyLifetime.Singleton);

      ResourceSpace.Has.ResourcesOfType<IssueResource>()
        .AtUri(UrlPaths.Issue)
        .HandledBy<IssueHandler>()
        .TranscodedBy<IssueCodec>();

      ResourceSpace.Has.ResourcesOfType<IssueQueryResource>()
        .AtUri(UrlPaths.IssueQuery)
        .HandledBy<IssueQueryHandler>()
        .TranscodedBy<IssueQueryCodec>();

      ResourceSpace.Has.ResourcesOfType<IssueCollectionResource>()
        .AtUri(UrlPaths.Issues)
        .HandledBy<IssuesHandler>();

      ResourceSpace.Has.ResourcesOfType<CreateIssueArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<CreateIssueArgs>>();
    }
  }
}
