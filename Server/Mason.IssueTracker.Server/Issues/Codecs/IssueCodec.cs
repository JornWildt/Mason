using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.Net;


namespace Mason.IssueTracker.Server.IssueTracker.Codecs
{
  public class IssueCodec : IssueTrackerMasonCodec<IssueResource>
  {
    protected override Resource ConvertToIssueTracker(IssueResource issue)
    {
      dynamic i = new Resource();

      i.SetMeta(MasonProperties.MetaProperties.Title, issue.Issue.Title);
      i.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single issue with its data and related actions.");

      i.ID = issue.Issue.Id;
      i.Title = issue.Issue.Title;
      i.Description = issue.Issue.Description;

      return i;
    }
  }
}
