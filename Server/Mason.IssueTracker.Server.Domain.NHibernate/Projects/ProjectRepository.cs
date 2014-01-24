using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.Projects;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Projects
{
  public class ProjectRepository : NHibernateGenericRepository<Project, int>, IProjectRepository
  {
    #region Dependencies

    public IIssueRepository IssueRepository { get; set; }

    #endregion


    public override void Delete(Project project)
    {
      foreach (Issue i in IssueRepository.IssuesForProject(project.Id))
        IssueRepository.Delete(i);
      base.Delete(project);
    }
  }
}
