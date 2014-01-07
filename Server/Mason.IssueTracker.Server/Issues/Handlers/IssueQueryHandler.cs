using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Domain.Issues;
using OpenRasta.Web;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssueQueryHandler
  {
    #region Dependencies

    public IIssueRepository IssueRepository { get; set; }

    #endregion


    public object Get(int? id=null, string text=null)
    {
      IssueSearchArgs args = new IssueSearchArgs { Id = id, TextQuery = text };

      Uri selfUri = typeof(IssueQueryResource).CreateUri(new { id = id, text = text });

      List<Issue> issues = IssueRepository.FindIssues(args);
      return new Resources.IssueQueryResource
      {
        Issues = issues,
        SelfUri = selfUri
      };
    }
  }
}
