using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.IssueTracker.Server.ResourceCommons.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.ResourceCommons.Handlers
{
  public class ResourceCommonHandler
  {
    public ICommunicationContext Context { get; set; }


    public object Get()
    {
      dynamic common = new Resource();
      common.Title = Settings.OriginName;
      common.Description = Settings.OriginDescription;

      Uri selfUri = typeof(ResourceCommonResource).CreateUri();
      Link selfLink = new Link("self", selfUri);
      common.AddLink(selfLink);

      Uri contactUri = typeof(ContactResource).CreateUri();
      Link contactLink = new Link(RelTypes.Contact, contactUri, "Complete contact information in standard formats such as vCard and jCard");
      common.AddLink(contactLink);

      Uri logoUri = new Uri(Context.ApplicationBaseUri.EnsureHasTrailingSlash(), "Origins/MinistryOfFun/logo.png");
      Link logoLink = new Link(RelTypes.Logo, logoUri);
      common.AddLink(logoLink);

      common.SetMeta(MasonProperties.MetaProperties.Title, "Common resource data for " + Settings.OriginName);
      common.SetMeta(MasonProperties.MetaProperties.Description, "This resource contains common information for all resources (such as common links, implementation and owner details).");
      common.AddMetaLink(new Link("documentation", "http://www.dr.dk", "Documentation (NOT READY)"));

      Uri projectsUri = typeof(ProjectCollectionResource).CreateUri();
      Link projectsLink = new Link(RelTypes.Projects, projectsUri, "List all projects");
      common.AddLink(projectsLink);

      string issueQueryUrl = Context.ApplicationBaseUri.AbsoluteUri + "/" + UrlPaths.IssueQuery;
      LinkTemplate issueQueryTemplate = new LinkTemplate(RelTypes.IssueQuery, issueQueryUrl, "Search for issues");
      issueQueryTemplate.parameters.Add(new LinkTemplateParameter("id", description: "Issue ID"));
      issueQueryTemplate.parameters.Add(new LinkTemplateParameter("text", description: "Text query searching all relevante issue properties"));
      common.AddLinkTemplate(issueQueryTemplate);

      Uri projectsUrl = typeof(ProjectCollectionResource).CreateUri();
      Uri createProjectSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-project" });
      Net.Action addProjectAction = new Net.Action(RelTypes.CreateProject, "json", projectsUrl, "Create new project", schemaUrl: createProjectSchemaUrl);
      common.AddAction(addProjectAction);

      return new ResourceCommonResource { Value = common };
    }
  }
}
