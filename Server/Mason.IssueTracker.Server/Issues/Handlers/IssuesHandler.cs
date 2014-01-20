using log4net;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Domain.Projects;
using Mason.IssueTracker.Server.Issues.Resources;
using OpenRasta.Web;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssuesHandler : BaseHandler
  {
    static ILog Logger = LogManager.GetLogger(typeof(IssuesHandler));

    #region Dependencies

    public IIssueRepository IssueRepository { get; set; }
    public IProjectRepository ProjectRepository { get; set; }

    #endregion


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        List<Issue> issues = IssueRepository.IssuesForProject(id);
        return new IssueCollectionResource
        {
          Issues = issues
        };
      });
    }


    public object Post(int id, CreateIssueArgs args)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Project p = ProjectRepository.Get(id);
        Issue i = new Issue(p, args.Title, args.Description, args.Severity);
        IssueRepository.Add(i);

        Uri issueUrl = typeof(IssueResource).CreateUri(new { id = i.Id });

        return new OperationResult.Created { RedirectLocation = issueUrl };
      });
    }
  }
}
