namespace Mason.IssueTracker.Server.Domain.Projects
{
  public interface IProjectRepository
  {
    void Add(Project p);
    Project Get(int id);
    void Delete(Project p);
  }
}
