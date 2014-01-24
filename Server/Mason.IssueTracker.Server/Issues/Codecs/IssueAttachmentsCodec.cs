using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mason.IssueTracker.Server.Issues.Codecs
{
  public class IssueAttachmentsCodec : IssueTrackerMasonCodec<IssueAttachmentsResource>
  {
    protected override Resource ConvertToIssueTracker(IssueAttachmentsResource issue)
    {
      dynamic i = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        i.SetMeta(MasonProperties.MetaProperties.Title, "Issue attachments");
        i.SetMeta(MasonProperties.MetaProperties.Description, "This is the list of attachments for a single issue.");
      }

      Uri selfUrl = typeof(IssueAttachmentsResource).CreateUri(new { id = issue.Issue.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl);
      i.AddLink(selfLink);

      Uri issueUrl = typeof(IssueResource).CreateUri(new { id = issue.Issue.Id });
      Link issueLink = CommunicationContext.NewLink("up", issueUrl);
      i.AddLink(issueLink);

      i.Id = issue.Issue.Id;
      i.Title = issue.Issue.Title;

      i.Attachments = new List<SubResource>();

      foreach (Attachment a in issue.Attachments)
      {
        dynamic item = new SubResource();
        item.ID = a.Id.ToString();
        item.Title = a.Title;

        Uri itemSelfUri = typeof(AttachmentResource).CreateUri(new { id = a.Id });
        Link itemSelfLink = CommunicationContext.NewLink("self", itemSelfUri);
        item.AddLink(itemSelfLink);

        i.Attachments.Add(item);
      }

      return i;
    }
  }
}
