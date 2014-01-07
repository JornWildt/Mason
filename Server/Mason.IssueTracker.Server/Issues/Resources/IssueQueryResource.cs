using Mason.IssueTracker.Server.Domain.Issues;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Resources
{
  public class IssueQueryResource
  {
    public List<Issue> Issues { get; set; }
    
    public Uri SelfUri { get; set; }
  }
}
