using FluentNHibernate.Mapping;
using Mason.IssueTracker.Server.Domain.Issues;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Issues
{
  public class IssueMapping : ClassMap<Issue>
  {
    public IssueMapping()
    {
      Id(p => p.Id);
      References(p => p.OwnerProject).Column("Project_Id");
      Map(p => p.Title).CustomType("AnsiString").Length(255);
      Map(p => p.Description);
      Map(p => p.Severity);
      Map(p => p.CreatedDate);
    }
  }
}
