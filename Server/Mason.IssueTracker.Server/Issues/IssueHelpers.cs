using MasonBuilder.Net;
using OpenRasta.Web;


namespace Mason.IssueTracker.Server.Issues
{
  public static class IssueHelpers
  {
    public static Control BuildIssueQueryTemplate(this IMasonBuilderContext masonContext, ICommunicationContext communicationContext)
    {
      string issueQueryUrl = communicationContext.ApplicationBaseUri.AbsoluteUri + "/" + UrlPaths.IssueQuery;
      Control issueQueryTemplate = masonContext.NewLinkTemplate(RelTypes.IssueQuery, issueQueryUrl, "Search for issues", "This is a simple search that do not check attachments.");
      if (!masonContext.PreferMinimalResponse)
      {
        issueQueryTemplate.schema = new
        {
          properties = new
          {
            text = new
            {
              description = "Substring search for text in title and description",
              type = "string"
            },
            severity = new
            {
              description = "Issue severity (exact value, 1..5)",
              type = "string"
            },
            pid = new
            {
              description = "Project ID",
              type = "string"
            }
          }
        };
        // FIXME - schema
        //issueQueryTemplate.AddHrefTemplateParameter(new HrefTemplateParameter("text", description: "Substring search for text in title and description"));
        //issueQueryTemplate.AddHrefTemplateParameter(new HrefTemplateParameter("severity", description: "Issue severity (exact value, 1..5)"));
        //issueQueryTemplate.AddHrefTemplateParameter(new HrefTemplateParameter("pid", description: "Project ID"));
      }

      return issueQueryTemplate;
    }
  }
}
