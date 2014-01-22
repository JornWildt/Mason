using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Domain.Attachments;


namespace Mason.IssueTracker.Server.Attachments.Handlers
{
  public class AttachmentHandler : BaseHandler
  {
    public IAttachmentRepository AttachmentRepository { get; set; }


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Domain.Attachments.Attachment a = AttachmentRepository.Get(id);
        return new AttachmentResource
        {
          Attachment = a
        };
      });
    }
  }
}
