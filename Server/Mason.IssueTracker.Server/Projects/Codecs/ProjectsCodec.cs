using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.Net;


namespace Mason.IssueTracker.Server.Projects.Codecs
{
  public class ProjectsCodec : IssueTrackerMasonCodec<ProjectCollectionResource>
  {
    protected override Net.Resource ConvertToIssueTracker(ProjectCollectionResource project)
    {
      Contract.Project p = new Contract.Project(); // FIXME - wrong

      p.SetMeta(MasonProperties.MetaProperties.Title, "Project list");
      p.SetMeta(MasonProperties.MetaProperties.Description, "List of all projects.");


      return p;
    }
  }
}
