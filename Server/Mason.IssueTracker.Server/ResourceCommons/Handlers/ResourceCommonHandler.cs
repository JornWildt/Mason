using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.Issues;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.IssueTracker.Server.ResourceCommons.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.ResourceCommons.Handlers
{
  public class ResourceCommonHandler : BaseHandler
  {
    public object Get()
    {
      dynamic common = new Resource();
      common.Title = Settings.OriginName;
      common.Description = "Example of an IssueTracker service using Mason media type.";

      Uri selfUri = typeof(ResourceCommonResource).CreateUri();
      Link selfLink = CommunicationContext.NewLink("self", selfUri);
      common.AddLink(selfLink);

      Uri contactUri = typeof(ContactResource).CreateUri();
      Link contactLink = CommunicationContext.NewLink(RelTypes.Contact, contactUri, "Complete contact information in standard formats such as vCard and jCard");
      common.AddLink(contactLink);

      Uri logoUri = new Uri(CommunicationContext.ApplicationBaseUri.EnsureHasTrailingSlash(), "Origins/JoeHacker/logo.png");
      Link logoLink = CommunicationContext.NewLink(RelTypes.Logo, logoUri);
      common.AddLink(logoLink);

      if (!CommunicationContext.PreferMinimalResponse())
      {
        common.Meta.Title = "Common resource data for " + Settings.OriginName;
        common.Meta.Description = "This resource contains common information for all resources (such as common links, implementation and owner details).";
      }

      Uri projectsUri = typeof(ProjectCollectionResource).CreateUri();
      Link projectsLink = CommunicationContext.NewLink(RelTypes.Projects, projectsUri, "List all projects");
      common.AddLink(projectsLink);

      common.AddLinkTemplate(CommunicationContext.BuildIssueQueryTemplate());

      Uri projectsUrl = typeof(ProjectCollectionResource).CreateUri();
      Uri createProjectSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-project" });
      Net.Action addProjectAction = CommunicationContext.NewAction(RelTypes.ProjectAdd, "json", projectsUrl, "Create new project", schemaUrl: createProjectSchemaUrl);
      common.AddAction(addProjectAction);

      return new ResourceCommonResource { Value = common };
    }
  }
}
