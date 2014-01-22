using FluentNHibernate.Mapping;
using Mason.IssueTracker.Server.Domain.Attachments;


namespace Mason.AttachmentTracker.Server.Domain.NHibernate.Attachments
{
  public class AttachmentMapping : ClassMap<Attachment>
  {
    public AttachmentMapping()
    {
      Id(a => a.Id);
      References(a => a.OwnerIssue).Column("Issue_Id");
      Map(a => a.Title).CustomType("AnsiString").Length(255);
      Map(a => a.Description);
      Map(a => a.Content).LazyLoad();
      Map(a => a.ContentType);
      Map(a => a.ContentLength);
      Map(a => a.CreatedDate);
    }
  }
}
