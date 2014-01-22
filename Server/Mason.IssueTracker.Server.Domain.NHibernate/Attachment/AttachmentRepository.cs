using Mason.IssueTracker.Server.Domain.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Domain.NHibernate.Attachments
{
  public class AttachmentRepository : NHibernateGenericRepository<Attachment, int>, IAttachmentRepository
  {
    #region IAttachmentRepository Members

    public List<Attachment> AttachmentsForIssue(int issueId)
    {
      var query = Query().Where(i => i.OwnerIssue.Id == issueId);
      return query.ToList();
    }

    #endregion
  }
}
