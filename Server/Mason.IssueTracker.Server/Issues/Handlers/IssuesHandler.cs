using log4net;
using Mason.IssueTracker.Server.Issues.Resources;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssuesHandler
  {
    static ILog Logger = LogManager.GetLogger(typeof(IssuesHandler));


    public object Post(CreateIssueArgs args)
    {
      Uri newIssueUrl = typeof(IssueResource).CreateUri(new { id = 1 });
      return new OperationResult.Created { RedirectLocation = newIssueUrl };
    }
  }
}
