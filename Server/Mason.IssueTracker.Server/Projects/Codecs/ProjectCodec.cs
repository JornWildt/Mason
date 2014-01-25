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
      dynamic p = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        p.SetMeta(MasonProperties.MetaProperties.Title, "Project");
        p.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single project with its data and related actions.");
      }

      Uri selfUrl = typeof(ProjectResource).CreateUri(new { id = project.Project.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl, "Project details");
      p.AddLink(selfLink);

      Uri issuesUrl = typeof(ProjectIssuesResource).CreateUri(new { id = project.Project.Id });
      Link issuesLink = CommunicationContext.NewLink("is:issues", issuesUrl, "All issues in project");
      p.AddLink(issuesLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Code = project.Project.Code;
      updateTemplate.Title = project.Project.Title;
      updateTemplate.Description = project.Project.Description;

      Net.Action updateAction = CommunicationContext.NewAction("is:update-project", MasonProperties.ActionTypes.JSON, selfUrl, "Update project details", template: (DynamicDictionary)updateTemplate);
      p.AddAction(updateAction);

      Uri addIssueSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-issue" });
      Net.Action addIssueAction = CommunicationContext.NewAction("is:add-issue", MasonProperties.ActionTypes.JSONFiles, issuesUrl, "Add new issue to project", schemaUrl: addIssueSchemaUrl);
      if (!CommunicationContext.PreferMinimalResponse())
      {
        addIssueAction.jsonFile = "args";
        addIssueAction.AddFile("attachment", "Attachment for issue");
      }
      p.AddAction(addIssueAction);

      Net.Action deleteAction = CommunicationContext.NewAction("is:delete-project", MasonProperties.ActionTypes.Void, selfUrl, "Delete project", method: "DELETE");
      p.AddAction(deleteAction);

      p.Id = project.Project.Id;
      p.Code = project.Project.Code;
      p.Title = project.Project.Title;
      p.Description = project.Project.Description;

      return p;
    }
  }
}
