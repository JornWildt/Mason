using FluentNHibernate.Mapping;
using Mason.IssueTracker.Server.Domain.Projects;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Projects
{
  public class ProjectMapping : ClassMap<Project>
  {
    public ProjectMapping()
    {
      Id(p => p.Id);
      Map(p => p.Code).CustomType("AnsiString").Length(20).Index("Idx_Project_Code").Unique();
      Map(p => p.Title).CustomType("AnsiString").Length(255);
      Map(p => p.Description);
    }
  }
}
