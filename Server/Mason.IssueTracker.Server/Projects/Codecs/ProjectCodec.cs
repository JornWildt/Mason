using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using MasonBuilder.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Projects.Codecs
{
  public class ProjectCodec : IssueTrackerMasonCodec<ProjectResource>
  {
    protected override Resource ConvertToIssueTracker(ProjectResource project)
    {
      Resource p = new Resource();

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        p.Meta.Title = "Project";
        p.Meta.Description = "This resource represents a single project with its data and related actions.";
      }

      Uri selfUrl = typeof(ProjectResource).CreateUri(new { id = project.Project.Id });
      Control selfLink = MasonBuilderContext.NewLink("self", selfUrl, "Project details");
      p.AddControl(selfLink);

      Uri issuesUrl = typeof(ProjectIssuesResource).CreateUri(new { id = project.Project.Id });
      Control issuesLink = MasonBuilderContext.NewLink(RelTypes.Issues, issuesUrl, "All issues in project");
      p.AddControl(issuesLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Code = project.Project.Code;
      updateTemplate.Title = project.Project.Title;
      updateTemplate.Description = project.Project.Description;

      Control updateAction = MasonBuilderContext.NewJsonAction(RelTypes.ProjectUpdate, selfUrl, "Update project details", template: (DynamicDictionary)updateTemplate);
      p.AddControl(updateAction);

      Uri addIssueSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-issue" });
      Control addIssueAction = MasonBuilderContext.NewJsonFilesAction(RelTypes.ProjectAddIssue, issuesUrl, "Add issue", "Add new issue to project", schemaUrl: addIssueSchemaUrl);
      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        addIssueAction.AddFile(new FileDefinition { name = "attachment", title = "Attachment", description = "Include attachment for new issue." });
      }
      p.AddControl(addIssueAction);

      Control deleteAction = MasonBuilderContext.NewVoidAction(RelTypes.ProjectDelete, selfUrl, "Delete project", "This will delete the whole project and all issues in it.", method: "DELETE");
      p.AddControl(deleteAction);

      ((dynamic)p).Id = project.Project.Id;
      ((dynamic)p).Code = project.Project.Code;
      ((dynamic)p).Title = project.Project.Title;
      ((dynamic)p).Description = project.Project.Description;

      return p;
    }
  }
}
