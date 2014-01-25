using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Domain.Issues;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using log4net;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssueQueryHandler
  {
    static ILog Logger = LogManager.GetLogger(typeof(IssueQueryHandler));


    #region Dependencies

    public IIssueRepository IssueRepository { get; set; }

    #endregion


    public object Get(string text=null, string severity=null, string pid=null)
    {
      // iseverity = integer severity (best known way to handle (ignore) empty parameter severity="")
      int? iseverity = null;
      if (!string.IsNullOrEmpty(severity))
        iseverity = int.Parse(severity);

      // ipid = integer project ID (best known way to handle (ignore) empty parameter pid="")
      int? ipid = null;
      if (!string.IsNullOrEmpty(pid))
        ipid = int.Parse(pid);

      IssueSearchArgs args = new IssueSearchArgs { TextQuery = text, Severity = iseverity, ProjectId = ipid };

      Uri selfUri = typeof(IssueQueryResource).CreateUri(new { text = text, severity = severity, pid = pid });

      List<Issue> issues = IssueRepository.FindIssues(args);
      return new Resources.IssueQueryResource
      {
        Issues = issues,
        SelfUri = selfUri
      };
    }
  }
}
