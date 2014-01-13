using Mason.IssueTracker.Server.Domain.Projects;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Projects.Resources
{
  public class ProjectCollectionResource
  {
    public List<Project> Projects { get; set; }
  }
}
