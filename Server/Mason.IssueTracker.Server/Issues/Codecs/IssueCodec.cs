using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.IssueTracker.Codecs
{
  public class IssueCodec : IssueTrackerMasonCodec<IssueResource>
  {
    protected override Resource ConvertToIssueTracker(IssueResource issue)
    {
      dynamic i = new Resource();

      i.SetMeta(MasonProperties.MetaProperties.Title, "Issue");
      i.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single issue with its data and related actions.");

      Uri selfUrl = typeof(IssueResource).CreateUri(new { id = issue.Issue.Id });
      Link selfLink = new Link("self", selfUrl);
      i.AddLink(selfLink);

      Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = issue.Issue.OwnerProject.Id });
      Link projectLink = new Link("up", projectUrl, "Containing project");
      i.AddLink(projectLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Title = issue.Issue.Title;
      updateTemplate.Description = issue.Issue.Description;
      updateTemplate.Severity = issue.Issue.Severity;

      Net.Action updateAction = new Net.Action("is:update-issue", MasonProperties.ActionTypes.JSON, selfUrl, "Update issue details", template: updateTemplate);
      i.AddAction(updateAction);
      
      Net.Action deleteAction = new Net.Action("is:delete-issue", MasonProperties.ActionTypes.Void, selfUrl, "Delete issue", method: "DELETE");
      i.AddAction(deleteAction);

      i.ID = issue.Issue.Id;
      i.Title = issue.Issue.Title;
      i.Description = issue.Issue.Description;
      i.Severity = issue.Issue.Severity;

      List<SubResource> attachments = new List<SubResource>();
      foreach (Attachment att in issue.Attachments)
      {
        dynamic a = new SubResource();
        a.Id = att.Id;
        a.Title = att.Title;

        Uri attachmentUrl = typeof(AttachmentResource).CreateUri(new { id = att.Id });
        Link attachmentLink = new Link("self", attachmentUrl);
        a.AddLink(attachmentLink);

        attachments.Add(a);
      }

      i.Attachments = attachments;

      return i;
    }
  }
}
