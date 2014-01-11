using log4net;
using Mason.IssueTracker.Server.Issues.Resources;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class CreateIssueArgs
  {
    public string a { get; set; }
  }

  public class IssuesHandler
  {
    static ILog Logger = LogManager.GetLogger(typeof(IssuesHandler));


    public object Post(CreateIssueArgs uganda)
    {
      Logger.Debug("GOT: " + uganda.a);
      Uri newIssueUrl = typeof(IssueResource).CreateUri(new { id = 1 });
      return new OperationResult.Created { RedirectLocation = newIssueUrl };
    }
  }
}
