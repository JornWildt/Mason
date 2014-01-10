using Mason.IssueTracker.Server.Issues.Resources;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssuesHandler
  {
    public object Post(string title)
    {
      Uri newIssueUrl = typeof(IssueResource).CreateUri(new { id = 1 });
      return new OperationResult.Created { CreatedResourceUrl = newIssueUrl };
    }
  }
}
