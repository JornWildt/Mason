using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Domain.Issues;
using System;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssueHandler
  {
    public IIssueRepository IssueRepository { get; set; }


    public object Get(long id)
    {
      Domain.Issues.Issue i = IssueRepository.Get(id);
      return new IssueResource
      {
        Issue = i
      };
    }
  }
}
