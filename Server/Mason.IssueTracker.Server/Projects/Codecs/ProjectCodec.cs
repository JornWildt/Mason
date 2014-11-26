using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Projects.Codecs
{
  public class ProjectCodec : IssueTrackerMasonCodec<ProjectResource>
  {
    protected override Resource ConvertToIssueTracker(ProjectResource project)
    {
      Resource p = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        p.Meta.Title = "Project";
        p.Meta.Description = "This resource represents a single project with its data and related actions.";
      }

      Uri selfUrl = typeof(ProjectResource).CreateUri(new { id = project.Project.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl, "Project details");
      p.AddNavigation(selfLink);

      Uri issuesUrl = typeof(ProjectIssuesResource).CreateUri(new { id = project.Project.Id });
      Link issuesLink = CommunicationContext.NewLink(RelTypes.Issues, issuesUrl, "All issues in project");
      p.AddNavigation(issuesLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Code = project.Project.Code;
      updateTemplate.Title = project.Project.Title;
      updateTemplate.Description = project.Project.Description;

      Net.JsonAction updateAction = CommunicationContext.NewJsonAction(RelTypes.ProjectUpdate, selfUrl, "Update project details", template: (DynamicDictionary)updateTemplate);
      p.AddNavigation(updateAction);

      Uri addIssueSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-issue" });
      Net.JsonFilesAction addIssueAction = CommunicationContext.NewJsonFilesAction(RelTypes.ProjectAddIssue, issuesUrl, "Add new issue to project", schemaUrl: addIssueSchemaUrl);
      if (!CommunicationContext.PreferMinimalResponse())
      {
        addIssueAction.jsonFile = "args";
        addIssueAction.AddFile("attachment", "Attachment for issue");
      }
      p.AddNavigation(addIssueAction);

      Net.JsonAction deleteAction = CommunicationContext.NewJsonAction(RelTypes.ProjectDelete, selfUrl, "Delete project", method: "DELETE");
      p.AddNavigation(deleteAction);

      ((dynamic)p).Id = project.Project.Id;
      ((dynamic)p).Code = project.Project.Code;
      ((dynamic)p).Title = project.Project.Title;
      ((dynamic)p).Description = project.Project.Description;

      return p;
    }
  }
}
