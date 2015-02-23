using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using MasonBuilder.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mason.IssueTracker.Server.Projects.Codecs
{
  public class ProjectIssuesCodec : IssueTrackerMasonCodec<ProjectIssuesResource>
  {
    protected override Resource ConvertToIssueTracker(ProjectIssuesResource project)
    {
      Resource p = new Resource();

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        p.Meta.Title = "Project issues";
        p.Meta.Description = "This is the list of issues for a single project.";
      }

      Uri selfUrl = typeof(ProjectIssuesResource).CreateUri(new { id = project.Project.Id });
      Control selfLink = MasonBuilderContext.NewLink("self", selfUrl);
      p.AddControl(selfLink);

      Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = project.Project.Id });
      Control projectLink = MasonBuilderContext.NewLink("up", projectUrl);
      p.AddControl(projectLink);

      ((dynamic)p).Id = project.Project.Id;
      ((dynamic)p).Title = project.Project.Title;

      ((dynamic)p).Issues = new List<SubResource>();

      foreach (Issue i in project.Issues)
      {
        dynamic item = new SubResource();
        item.ID = i.Id.ToString();
        item.Title = i.Title;

        Uri itemSelfUri = typeof(IssueResource).CreateUri(new { id = i.Id });
        Control itemSelfLink = MasonBuilderContext.NewLink("self", itemSelfUri);
        item.AddControl(itemSelfLink);

        ((dynamic)p).Issues.Add(item);
      }

      return p;
    }
  }
}
