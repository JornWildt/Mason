using MasonBuilder.Net;
using OpenRasta.Web;


namespace Mason.IssueTracker.Server.Issues
{
  public static class IssueHelpers
  {
    public static LinkTemplate BuildIssueQueryTemplate(this IMasonBuilderContext masonContext, ICommunicationContext communicationContext)
    {
      string issueQueryUrl = communicationContext.ApplicationBaseUri.AbsoluteUri + "/" + UrlPaths.IssueQuery;
      LinkTemplate issueQueryTemplate = masonContext.NewLinkTemplate(RelTypes.IssueQuery, issueQueryUrl, "Search for issues", "This is a simple search that do not check attachments.");
      if (!masonContext.PreferMinimalResponse)
      {
        issueQueryTemplate.AddParameter(new LinkTemplateParameter("text", description: "Substring search for text in title and description"));
        issueQueryTemplate.AddParameter(new LinkTemplateParameter("severity", description: "Issue severity (exact value, 1..5)"));
        issueQueryTemplate.AddParameter(new LinkTemplateParameter("pid", description: "Project ID"));
      }

      return issueQueryTemplate;
    }
  }
}
