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
    protected override Resource ConvertToIssueTracker(AttachmentResource attachment)
    {
      dynamic a = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        a.Meta.Title = "Attachment";
        a.Meta.Description = "This resource represents a single attachment with its data and related actions.";
      }

      Uri selfUrl = typeof(AttachmentResource).CreateUri(new { id = attachment.Attachment.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl);
      a.AddLink(selfLink);

      Uri issueUrl = typeof(IssueResource).CreateUri(new { id = attachment.Attachment.OwnerIssue.Id });
      Link issueLink = CommunicationContext.NewLink("up", issueUrl, "Containing issue");
      a.AddLink(issueLink);

      if (attachment.Attachment.ContentType != null)
      {
        Uri contentUrl = typeof(AttachmentContentResource).CreateUri(new { id = attachment.Attachment.Id });
        Link contentLink = CommunicationContext.NewLink(RelTypes.AttachmentContent, contentUrl, "Download content", attachment.Attachment.ContentType);
        a.AddLink(contentLink);
      }

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Title = attachment.Attachment.Title;
      updateTemplate.Description = attachment.Attachment.Description;

      Net.Action updateAction = CommunicationContext.NewAction(RelTypes.AttachmentUpdate, MasonProperties.ActionTypes.JSONFiles, selfUrl, "Update attachment details", description: "Update title and description of attachment", template: (DynamicDictionary)updateTemplate);
      if (!CommunicationContext.PreferMinimalResponse())
      {
        updateAction.jsonFile = "args";
        updateAction.AddFile("attachment", "Attachment content");
      }
      a.AddAction(updateAction);

      Net.Action deleteAction = CommunicationContext.NewAction(RelTypes.AttachmentDelete, MasonProperties.ActionTypes.Void, selfUrl, "Delete attachment", method: "DELETE");
      a.AddAction(deleteAction);

      a.ID = attachment.Attachment.Id;
      a.Title = attachment.Attachment.Title;
      a.Description = attachment.Attachment.Description;
      a.ContentType = attachment.Attachment.ContentType;
      a.ContentLength = attachment.Attachment.ContentLength;
      a.CreatedDate = attachment.Attachment.CreatedDate;

      return a;
    }
  }
}
