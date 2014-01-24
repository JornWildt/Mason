using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.Net;
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
      dynamic result = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        result.SetMeta(MasonProperties.MetaProperties.Title, "Project issues");
        result.SetMeta(MasonProperties.MetaProperties.Description, "This is the list of issues for a single project.");
      }

      Uri selfUrl = typeof(ProjectIssuesResource).CreateUri(new { id = project.Project.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl);
      result.AddLink(selfLink);

      Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = project.Project.Id });
      Link projectLink = CommunicationContext.NewLink("up", projectUrl);
      result.AddLink(projectLink);

      result.Id = project.Project.Id;
      result.Title = project.Project.Title;

      result.Issues = new List<SubResource>();

      foreach (Issue i in project.Issues)
      {
        dynamic item = new SubResource();
        item.ID = i.Id.ToString();
        item.Title = i.Title;

        Uri itemSelfUri = typeof(IssueResource).CreateUri(new { id = i.Id });
        Link itemSelfLink = CommunicationContext.NewLink("self", itemSelfUri);
        item.AddLink(itemSelfLink);

        result.Issues.Add(item);
      }

      return result;
    }
  }
}
