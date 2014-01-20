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
    protected override Net.Resource ConvertToIssueTracker(ProjectResource project)
    {
      Contract.Project p = new Contract.Project();

      p.SetMeta(MasonProperties.MetaProperties.Title, project.Project.Title);
      p.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single project with its data and related actions.");

      Uri selfUri = typeof(ProjectResource).CreateUri(new { id = project.Project.Id });
      Link selfLink = new Link("self", selfUri, "Project details");
      p.AddLink(selfLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Code = project.Project.Code;
      updateTemplate.Title = project.Project.Title;
      updateTemplate.Description = project.Project.Description;

      Uri issuesUrl = typeof(IssueCollectionResource).CreateUri(new { id = project.Project.Id });
      Link issuesLink = new Link("is:issues", issuesUrl, "All issues in project");
      p.AddLink(issuesLink);

      Net.Action updateAction = new Net.Action("is:update-project", "json", selfUri, "Update project details", template: updateTemplate);
      p.AddAction(updateAction);

      Uri addIssueSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-issue" });
      Net.Action addIssueAction = new Net.Action("is:add-issue", "json", issuesUrl, "Add new issue to project", schemaUrl: addIssueSchemaUrl);
      p.AddAction(addIssueAction);

      Net.Action deleteAction = new Net.Action("is:delete-project", MasonProperties.ActionTypes.Void, selfUri, "Delete project", method: "DELETE");
      p.AddAction(deleteAction);

      p.Id = project.Project.Id;
      p.Code = project.Project.Code;
      p.Title = project.Project.Title;
      p.Description = project.Project.Description;

      return p;
    }
  }
}
