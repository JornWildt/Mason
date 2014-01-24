using log4net;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.NHibernate.Projects;
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Projects.Codecs;
using Mason.IssueTracker.Server.Projects.Handlers;
using Mason.IssueTracker.Server.Projects.Resources;
using OpenRasta.Configuration;
using OpenRasta.DI;


namespace Mason.IssueTracker.Server.Projects
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting Projects");
      ResourceSpace.Uses.CustomDependency<IProjectRepository, ProjectRepository>(DependencyLifetime.Singleton);

      ResourceSpace.Has.ResourcesOfType<ProjectResource>()
        .AtUri(UrlPaths.Project)
        .HandledBy<ProjectHandler>()
        .TranscodedBy<ProjectCodec>();

      ResourceSpace.Has.ResourcesOfType<ProjectCollectionResource>()
        .AtUri(UrlPaths.Projects)
        .HandledBy<ProjectsHandler>()
        .TranscodedBy<ProjectsCodec>();

      ResourceSpace.Has.ResourcesOfType<ProjectIssuesResource>()
        .AtUri(UrlPaths.ProjectIssues)
        .HandledBy<ProjectIssuesHandler>()
        .TranscodedBy<ProjectIssuesCodec>();

      ResourceSpace.Has.ResourcesOfType<Contract.CreateProjectArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<Contract.CreateProjectArgs>>();

      ResourceSpace.Has.ResourcesOfType<Contract.UpdateProjectArgs>()
        .WithoutUri
        .TranscodedBy<JsonReader<Contract.UpdateProjectArgs>>();
    }
  }
}
