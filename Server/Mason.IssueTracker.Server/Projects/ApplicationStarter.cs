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
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Domain.NHibernate.Projects;


namespace Mason.IssueTracker.Server.Projects
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Projects");
      ResourceSpace.Uses.CustomDependency<IProjectRepository, ProjectRepository>(DependencyLifetime.Singleton);

      //ResourceSpace.Has.ResourcesOfType<IssueResource>()
      //  .AtUri(UrlPaths.Issue)
      //  .HandledBy<IssueHandler>()
      //  .TranscodedBy<IssueCodec>();
    }
  }
}
