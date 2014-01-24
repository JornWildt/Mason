using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Domain.Issues;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Utility;
using OpenRasta.IO;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.IO;


namespace Mason.IssueTracker.Server.Issues.Handlers
{
  public class IssueAttachmentsHandler : BaseHandler
  {
    #region Dependencies

    public IIssueRepository IssueRepository { get; set; }
    public IAttachmentRepository AttachmentRepository { get; set; }

    #endregion


    // "id" is issue ID
    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Issue issue = IssueRepository.Get(id);
        List<Attachment> attachments = AttachmentRepository.AttachmentsForIssue(id);
        return new IssueAttachmentsResource
        {
          Issue = issue,
          Attachments = attachments
        };
      });
    }

    
    // "id" is issue ID
    public object Post(int id, AddAttachmentArgs args, IFile attachment = null)
    {
      return ExecuteInUnitOfWork(() =>
        {
          Issue issue = IssueRepository.Get(id);
          Attachment att;
          if (attachment != null)
          {
            using (Stream s = attachment.OpenStream())
            {
              byte[] content = s.ReadAllBytes();
              att = new Attachment(issue, args.Title, args.Description, content, attachment.ContentType.MediaType);
            }
          }
          else
          {
            att = new Attachment(issue, args.Title, args.Description, null, null);
          }

          AttachmentRepository.Add(att);

          Uri attUrl = typeof(AttachmentResource).CreateUri(new { id = att.Id });

          return new OperationResult.Created { RedirectLocation = attUrl };
        });
    }
  }
}
