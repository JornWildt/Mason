using Mason.IssueTracker.Server.Domain.Issues;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Issues
{
  public class IssueRepository : NHibernateGenericRepository<Issue, int>, IIssueRepository
  {
    #region IIssueRepository Members

    public List<Issue> IssuesForProject(int projectId)
    {
      var query = Query().Where(i => i.OwnerProject.Id == projectId);
      return query.ToList();
    }


    public List<Issue> FindIssues(IssueSearchArgs args)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
