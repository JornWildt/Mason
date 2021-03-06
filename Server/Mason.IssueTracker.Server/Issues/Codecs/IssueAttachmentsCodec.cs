﻿using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Issues.Resources;
using MasonBuilder.Net;
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
      Resource i = new Resource();

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        i.Meta.Title = "Issue attachments";
        i.Meta.Description = "This is the list of attachments for a single issue.";
      }

      Uri selfUrl = typeof(IssueAttachmentsResource).CreateUri(new { id = issue.Issue.Id });
      Control selfLink = MasonBuilderContext.NewLink("self", selfUrl);
      i.AddControl(selfLink);

      Uri issueUrl = typeof(IssueResource).CreateUri(new { id = issue.Issue.Id });
      Control issueLink = MasonBuilderContext.NewLink("up", issueUrl);
      i.AddControl(issueLink);

      ((dynamic)i).Id = issue.Issue.Id;
      ((dynamic)i).Title = issue.Issue.Title;

      ((dynamic)i).Attachments = new List<SubResource>();

      foreach (Attachment a in issue.Attachments)
      {
        SubResource item = new SubResource();
        ((dynamic)item).ID = a.Id.ToString();
        ((dynamic)item).Title = a.Title;

        Uri itemSelfUri = typeof(AttachmentResource).CreateUri(new { id = a.Id });
        Control itemSelfLink = MasonBuilderContext.NewLink("self", itemSelfUri);
        item.AddControl(itemSelfLink);

        Uri contentUrl = typeof(AttachmentContentResource).CreateUri(new { id = a.Id });
        Control contentLink = MasonBuilderContext.NewLink(RelTypes.AttachmentContent, contentUrl, "Download content", a.ContentType);
        item.AddControl(contentLink);

        ((dynamic)i).Attachments.Add(item);
      }

      return i;
    }
  }
}
