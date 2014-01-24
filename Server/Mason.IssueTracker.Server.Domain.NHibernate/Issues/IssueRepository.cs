using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Domain.Issues;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Issues
{
  public class IssueRepository : NHibernateGenericRepository<Issue, int>, IIssueRepository
  {
    #region Dependencis

    public IAttachmentRepository AttachmentRepository { get; set; }

    #endregion


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


    public override void Delete(Issue issue)
    {
      foreach (Attachment a in AttachmentRepository.AttachmentsForIssue(issue.Id))
        AttachmentRepository.Delete(a);
      base.Delete(issue);
    }

    #endregion
  }
}
