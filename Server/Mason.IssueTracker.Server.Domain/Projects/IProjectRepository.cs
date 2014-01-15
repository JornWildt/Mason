using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Domain.Projects
{
  public interface IProjectRepository
  {
    void Add(Project p);
    Project Get(int id);
    IEnumerable<Project> FindAll();
    void Delete(Project p);
  }
}
