using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Projects.Resources;


namespace Mason.IssueTracker.Server.Projects.Handlers
{
  public class ProjectHandler : BaseHandler
  {
    #region Dependencies

    public IProjectRepository ProjectRepository { get; set; }

    #endregion


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Project p = ProjectRepository.Get(id);
        return new ProjectResource
        {
          Project = p
        };
      });
    }
  }
}
