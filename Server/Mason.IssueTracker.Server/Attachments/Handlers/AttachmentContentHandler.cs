using Mason.IssueTracker.Server.Domain.Attachments;
using OpenRasta.IO;
using OpenRasta.Web;
using System.IO;


namespace Mason.IssueTracker.Server.Attachments.Handlers
{
  public class AttachmentContentHandler : BaseHandler
  {
    public IAttachmentRepository AttachmentRepository { get; set; }


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Domain.Attachments.Attachment a = AttachmentRepository.Get(id);
        return new InMemoryFile(new MemoryStream(a.Content)) { ContentType = new MediaType(a.ContentType) };
      });
    }
  }
}
