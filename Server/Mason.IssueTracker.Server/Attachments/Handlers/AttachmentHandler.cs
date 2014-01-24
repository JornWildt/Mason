using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Utility;
using OpenRasta.IO;
using OpenRasta.Web;
using System.IO;


namespace Mason.IssueTracker.Server.Attachments.Handlers
{
  public class AttachmentHandler : BaseHandler
  {
    public IAttachmentRepository AttachmentRepository { get; set; }


    public object Get(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        return ReadAttachmentResource(id);
      });
    }


    public object Post(int id, UpdateAttachmentArgs args, IFile attachment = null)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Attachment a = AttachmentRepository.Get(id);
        if (attachment == null)
          a.Update(args.Title, args.Description);
        else
        {
          // Kick NHibernate in the b*lls to make sure lazy loaded property is loaded and updated
          byte[] dummy = a.Content;

          using (Stream s = attachment.OpenStream())
          {
            byte[] content = s.ReadAllBytes();
            a.Update(args.Title, args.Description, content, attachment.ContentType.ToString());
          }
        }
        return ReadAttachmentResource(id);
      });
    }

    
    public object Delete(int id)
    {
      return ExecuteInUnitOfWork(() =>
      {
        Attachment a = AttachmentRepository.Get(id);
        AttachmentRepository.Delete(a);
        return new OperationResult.NoContent();
      });
    }


    private object ReadAttachmentResource(int id)
    {
      Attachment a = AttachmentRepository.Get(id);
      return new AttachmentResource
      {
        Attachment = a
      };
    }
  }
}
