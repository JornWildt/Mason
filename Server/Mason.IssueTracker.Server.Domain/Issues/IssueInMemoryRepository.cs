using Mason.IssueTracker.Server.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mason.IssueTracker.Server.Domain.Issues
{
  public class IssueInMemoryRepository : IIssueRepository
  {
    #region IIssueRepository Members

    public Issue Get(long id)
    {
      Issue i = Read(id);
      if (i == null)
        throw new MissingResourceException("Unknown issue ID {0}.", id);
      return i;
    }

    public List<Issue> FindIssues(IssueSearchArgs args)
    
    {
      var q = Issues.Where(i =>
        (args.Id == null || i.Id == args.Id)
        && (string.IsNullOrEmpty(args.TextQuery) || IssueContainsText(i, args.TextQuery)));

      return q.ToList();
    }


    public void Add(Issue i)
    {
      i.Id = ++IssueNumber;
      Issues.Add(i);
    }

    #endregion


    protected static int IssueNumber { get; set; }
    protected static List<Issue> Issues { get; set; }


    static IssueInMemoryRepository()
    {
      Issues = new List<Issue>();
    }


    private Issue Read(long id)
    {
      Issue issue = Issues.Where(i => i.Id == id).FirstOrDefault();
      return issue;
    }


    private bool IssueContainsText(Issue i, string text)
    {
      return i != null && text != null &&
        (!string.IsNullOrEmpty(i.Title) && i.Title.ToLower().Contains(text.ToLower())
        || !string.IsNullOrEmpty(i.Description) && i.Description.ToLower().Contains(text.ToLower()));
    }
  }
}
