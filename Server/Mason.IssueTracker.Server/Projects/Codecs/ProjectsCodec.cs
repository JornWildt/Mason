using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Projects.Resources;
using MasonBuilder.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Projects.Codecs
{
  public class ProjectsCodec : IssueTrackerMasonCodec<ProjectCollectionResource>
  {
    protected override Resource ConvertToIssueTracker(ProjectCollectionResource projects)
    {
      Resource pcol = new Resource();

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        pcol.Meta.Title = "Project list";
        pcol.Meta.Description = "List of all projects.";
      }

      ((dynamic)pcol).Projects = new List<SubResource>(
        projects.Projects.Select(p => BuildProjectSubResource(p)));

      return pcol;
    }


    private SubResource BuildProjectSubResource(Project p)
    {
      SubResource sp = new SubResource();

      Uri selfUri = typeof(ProjectResource).CreateUri(new { id = p.Id });
      Link selfLink = MasonBuilderContext.NewLink("self", selfUri, "Project details");
      sp.AddControl(selfLink);

      dynamic dp = sp;
      dp.Id = p.Id;
      dp.Code = p.Code;
      dp.Title = p.Title;

      return sp;
    }
  }
}
