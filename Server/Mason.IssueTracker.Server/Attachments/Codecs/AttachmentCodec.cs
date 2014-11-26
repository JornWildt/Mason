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
      Resource a = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        a.Meta.Title = "Attachment";
        a.Meta.Description = "This resource represents a single attachment with its data and related actions.";
      }

      Uri selfUrl = typeof(AttachmentResource).CreateUri(new { id = attachment.Attachment.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl);
      a.AddNavigation(selfLink);

      Uri issueUrl = typeof(IssueResource).CreateUri(new { id = attachment.Attachment.OwnerIssue.Id });
      Link issueLink = CommunicationContext.NewLink("up", issueUrl, "Containing issue");
      a.AddNavigation(issueLink);

      if (attachment.Attachment.ContentType != null)
      {
        Uri contentUrl = typeof(AttachmentContentResource).CreateUri(new { id = attachment.Attachment.Id });
        Link contentLink = CommunicationContext.NewLink(RelTypes.AttachmentContent, contentUrl, "Download content", attachment.Attachment.ContentType);
        a.AddNavigation(contentLink);
      }

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Title = attachment.Attachment.Title;
      updateTemplate.Description = attachment.Attachment.Description;

      Net.JsonFilesAction updateAction = CommunicationContext.NewJsonFilesAction(RelTypes.AttachmentUpdate, selfUrl, "Update attachment details", description: "Update title and description of attachment", template: (DynamicDictionary)updateTemplate);
      if (!CommunicationContext.PreferMinimalResponse())
      {
        updateAction.jsonFile = "args";
        updateAction.AddFile("attachment", "Attachment content");
      }
      a.AddNavigation(updateAction);

      Net.JsonAction deleteAction = CommunicationContext.NewJsonAction(RelTypes.AttachmentDelete, selfUrl, "Delete attachment", method: "DELETE");
      a.AddNavigation(deleteAction);

      dynamic da = a;
      da.ID = attachment.Attachment.Id;
      da.Title = attachment.Attachment.Title;
      da.Description = attachment.Attachment.Description;
      da.ContentType = attachment.Attachment.ContentType;
      da.ContentLength = attachment.Attachment.ContentLength;
      da.CreatedDate = attachment.Attachment.CreatedDate;

      return a;
    }
  }
}
