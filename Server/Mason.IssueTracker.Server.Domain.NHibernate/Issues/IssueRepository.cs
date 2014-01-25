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
      var query = Query().Where(i =>
        (string.IsNullOrWhiteSpace(args.TextQuery) || i.Title.Contains(args.TextQuery) || i.Description.Contains(args.TextQuery))
        && (args.Severity == null || args.Severity == i.Severity)
        && (args.ProjectId == null || args.ProjectId == i.OwnerProject.Id));

      return query.ToList();
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
