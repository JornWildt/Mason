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


    public object Get(string id=null, string text=null)
    {
      Logger.DebugFormat("id = {0}", id);

      // iid = integer ID (best known way to handle (ignore) empty parameter id="")
      int? iid = null;
      if (!string.IsNullOrEmpty(id))
        iid = int.Parse(id);

      IssueSearchArgs args = new IssueSearchArgs { Id = iid, TextQuery = text };

      Uri selfUri = typeof(IssueQueryResource).CreateUri(new { id = iid, text = text });

      List<Issue> issues = IssueRepository.FindIssues(args);
      return new Resources.IssueQueryResource
      {
        Issues = issues,
        SelfUri = selfUri
      };
    }
  }
}
