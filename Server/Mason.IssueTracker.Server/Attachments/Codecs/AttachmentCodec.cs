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

      a.SetMeta(MasonProperties.MetaProperties.Title, att.Attachment.Title);
      a.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single attachment with its data and related actions.");

      Uri issueUrl = typeof(IssueResource).CreateUri(new { id = att.Attachment.OwnerIssue.Id });
      Link issueLink = new Link("up", issueUrl, "Containing issue");
      a.AddLink(issueLink);

      a.ID = att.Attachment.Id;
      a.Title = att.Attachment.Title;

      return a;
    }
  }
}
