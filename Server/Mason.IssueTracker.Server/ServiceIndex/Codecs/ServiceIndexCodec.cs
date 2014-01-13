using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.IssueTracker.Server.ServiceIndex.Resources;
using Mason.IssueTracker.Server.Utility;
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

      s.SetMeta(MasonProperties.MetaProperties.Title, "Issue tracker service index for " + Settings.OriginName);
      s.SetMeta(MasonProperties.MetaProperties.Description, "This is the service index for a demonstration of how an issue tracker could be implemented using Mason. The service index defines links, link templates and similar to be consumed at runtime by a Mason compatible client.");

      s.AddMetaLink(new Link("documentation", "http://www.dr.dk", "Documentation (NOT READY)"));

      string issueQueryUrl = CommunicationContext.ApplicationBaseUri.AbsoluteUri +"/" + UrlPaths.IssueQuery;
      LinkTemplate issueQueryTemplate = new LinkTemplate(RelTypes.IssueQuery, issueQueryUrl, "Search for issues");
      issueQueryTemplate.parameters.Add(new LinkTemplateParameter("id", description: "Issue ID"));
      issueQueryTemplate.parameters.Add(new LinkTemplateParameter("text", description: "Text query searching all relevante issue properties"));
      s.AddLinkTemplate(issueQueryTemplate);


      Uri projectsUrl = typeof(ProjectCollectionResource).CreateUri();
      Net.Action addProjectAction = new Net.Action(RelTypes.CreateProject, "json", projectsUrl.AbsoluteUri, "Create new project");
      s.AddAction(addProjectAction);

      //Uri issuesUrl = typeof(IssueCollectionResource).CreateUri();
      //Net.Action addIssueAction = new Net.Action(RelTypes.CreateIssue, "multipart-json", issuesUrl.AbsoluteUri, "Create new issue I.");
      //s.AddAction(addIssueAction);

      //// Non-files "add issue"
      //addIssueAction = new Net.Action(RelTypes.CreateIssue, "json", issuesUrl.AbsoluteUri, "Create new issue II.", createIssueSchema);
      //s.AddAction(addIssueAction);

      return s;
    }


    const string createIssueSchema = @"
{
  title: ""Issue creation arguments"",
  type: ""object"",
  properties:
  {
    ""Title"": { type: ""string"" },
    ""Description"": { type: ""string"" },
    ""Severity"": { type: ""integer"" }
  }
}";

  }
}
