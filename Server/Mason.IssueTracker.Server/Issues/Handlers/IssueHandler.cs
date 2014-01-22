using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Domain.Issues;
using System;
using Mason.IssueTracker.Server.Domain.Attachments;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssueHandler : BaseHandler
  {
    public IIssueRepository IssueRepository { get; set; }
    public IAttachmentRepository AttachmentRepository { get; set; }


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
        {
          Issue i = IssueRepository.Get(id);
          List<Attachment> attachments = AttachmentRepository.AttachmentsForIssue(id);
          return new IssueResource
          {
            Issue = i,
            Attachments = attachments
          };
        });
    }
  }
}
