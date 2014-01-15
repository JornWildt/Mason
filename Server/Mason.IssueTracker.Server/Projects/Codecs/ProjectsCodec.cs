using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Projects.Codecs
{
  public class ProjectsCodec : IssueTrackerMasonCodec<ProjectCollectionResource>
  {
    protected override Net.Resource ConvertToIssueTracker(ProjectCollectionResource projects)
    {
      Contract.ProjectCollection pcol = new Contract.ProjectCollection();

      pcol.SetMeta(MasonProperties.MetaProperties.Title, "Project list");
      pcol.SetMeta(MasonProperties.MetaProperties.Description, "List of all projects.");

      pcol.Projects = new List<SubResource>(
        projects.Projects.Select(p => BuildProjectSubResource(p)));

      return pcol;
    }


    private SubResource BuildProjectSubResource(Project p)
    {
      SubResource sp = new SubResource();

      Uri selfUri = typeof(ProjectResource).CreateUri(new { id = p.Id });
      Link selfLink = new Link("self", selfUri, "Project details");
      sp.AddLink(selfLink);

      dynamic dp = sp;
      dp.Id = p.Id;
      dp.Code = p.Code;
      dp.Title = p.Title;

      return sp;
    }
  }
}
