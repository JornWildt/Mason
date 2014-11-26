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
      Resource common = new Resource();
      ((dynamic)common).Title = Settings.OriginName;
      ((dynamic)common).Description = "Example of an IssueTracker service using Mason media type.";

      Uri selfUri = typeof(ResourceCommonResource).CreateUri();
      Link selfLink = CommunicationContext.NewLink("self", selfUri);
      common.AddNavigation(selfLink);

      Uri contactUri = typeof(ContactResource).CreateUri();
      Link contactLink = CommunicationContext.NewLink(RelTypes.Contact, contactUri, "Complete contact information in standard formats such as vCard and jCard");
      common.AddNavigation(contactLink);

      Uri logoUri = new Uri(CommunicationContext.ApplicationBaseUri.EnsureHasTrailingSlash(), "Origins/JoeHacker/logo.png");
      Link logoLink = CommunicationContext.NewLink(RelTypes.Logo, logoUri);
      common.AddNavigation(logoLink);

      if (!CommunicationContext.PreferMinimalResponse())
      {
        common.Meta.Title = "Common resource data for " + Settings.OriginName;
        common.Meta.Description = "This resource contains common information for all resources (such as common links, implementation and owner details).";
      }

      Uri projectsUri = typeof(ProjectCollectionResource).CreateUri();
      Link projectsLink = CommunicationContext.NewLink(RelTypes.Projects, projectsUri, "List all projects");
      common.AddNavigation(projectsLink);

      common.AddNavigation(CommunicationContext.BuildIssueQueryTemplate());

      Uri projectsUrl = typeof(ProjectCollectionResource).CreateUri();
      Uri createProjectSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-project" });
      Net.JsonAction addProjectAction = CommunicationContext.NewJsonAction(RelTypes.ProjectAdd, projectsUrl, "Create new project", schemaUrl: createProjectSchemaUrl);
      common.AddNavigation(addProjectAction);

      return new ResourceCommonResource { Value = common };
    }
  }
}
