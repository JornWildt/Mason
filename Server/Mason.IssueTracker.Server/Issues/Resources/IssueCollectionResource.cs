using Mason.IssueTracker.Server.Domain.Issues;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Resources
{
  public class IssueCollectionResource
  {
    public List<Issue> Issues { get; set; }
  }
}
