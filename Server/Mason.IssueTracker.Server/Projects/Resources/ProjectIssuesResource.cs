using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.Projects;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Projects.Resources
{
  public class ProjectIssuesResource
  {
    public Project Project { get; set; }
    public List<Issue> Issues { get; set; }
  }
}
