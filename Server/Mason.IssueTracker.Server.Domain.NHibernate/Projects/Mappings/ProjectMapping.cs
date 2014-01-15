using FluentNHibernate.Mapping;
using Mason.IssueTracker.Server.Domain.Projects;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Projects.Mappings
{
  public class ProjectMapping : ClassMap<Project>
  {
    public ProjectMapping()
    {
      Id(p => p.Id);
      Map(p => p.Code).Index("Idx_Project_Code").Unique();
      Map(p => p.Title);
      Map(p => p.Description);
    }
  }
}
