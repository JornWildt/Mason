using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Domain.Attachments
{
  public interface IAttachmentRepository
  {
    Attachment Get(int id);
    List<Attachment> AttachmentsForIssue(int issueId);
    void Add(Attachment i);
    void Delete(Attachment i);
  }
}
