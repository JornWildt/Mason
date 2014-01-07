using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.ServiceIndex.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.ServiceIndex.Codecs
{
  public class ServiceIndexCodec : IssueTrackerMasonCodec<ServiceIndexResource>
  {
    public ICommunicationContext CommunicationContext { get; set; }


    protected override Net.Resource ConvertToIssueTracker(ServiceIndexResource ServiceIndex)
    {
      Resource s = new Resource();

      s.SetMeta(MasonProperties.MetaProperties.Title, ServiceIndex.Title);
      s.SetMeta(MasonProperties.MetaProperties.Description, ServiceIndex.Description);

      string issueQueryUrl = CommunicationContext.ApplicationBaseUri.AbsoluteUri +"/" + UrlPaths.IssueQuery;
      LinkTemplate issueQueryTemplate = new LinkTemplate(RelTypes.IssueQuery, issueQueryUrl, "Search for issues");
      issueQueryTemplate.parameters.Add(new LinkTemplateParameter("id", description: "Issue ID"));
      issueQueryTemplate.parameters.Add(new LinkTemplateParameter("text", description: "Text query searching all relevante issue properties"));
      s.AddLinkTemplate(issueQueryTemplate);

      return s;
    }
  }
}
