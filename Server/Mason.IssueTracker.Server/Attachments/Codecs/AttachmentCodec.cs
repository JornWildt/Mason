using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Attachments.Codecs
{
  public class AttachmentCodec : IssueTrackerMasonCodec<AttachmentResource>
  {
    protected override Resource ConvertToIssueTracker(AttachmentResource att)
    {
      dynamic a = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        a.SetMeta(MasonProperties.MetaProperties.Title, "Attachment");
        a.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single attachment with its data and related actions.");
      }

      Uri issueUrl = typeof(IssueResource).CreateUri(new { id = att.Attachment.OwnerIssue.Id });
      Link issueLink = CommunicationContext.NewLink("up", issueUrl, "Containing issue");
      a.AddLink(issueLink);

      Uri contentUrl = typeof(AttachmentContentResource).CreateUri(new { id = att.Attachment.Id });
      Link contentLink = CommunicationContext.NewLink("is:content", contentUrl, "Download content", att.Attachment.ContentType);
      a.AddLink(contentLink);

      a.ID = att.Attachment.Id;
      a.Title = att.Attachment.Title;
      a.Description = att.Attachment.Description;
      a.ContentType = att.Attachment.ContentType;
      a.ContentLength = att.Attachment.ContentLength;
      a.CreatedDate = att.Attachment.CreatedDate;

      return a;
    }
  }
}
