using Mason.IssueTracker.Server.Domain.Projects;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Projects
{
  public class ProjectRepository : NHibernateGenericRepository<Project, int>, IProjectRepository
  {
  }
}
