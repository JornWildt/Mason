using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
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

      if (!CommunicationContext.PreferMinimalResponse())
      {
        i.SetMeta(MasonProperties.MetaProperties.Title, "Issue");
        i.SetMeta(MasonProperties.MetaProperties.Description, "This resource represents a single issue with its data and related actions.");
      }

      Uri selfUrl = typeof(IssueResource).CreateUri(new { id = issue.Issue.Id });
      Link selfLink = CommunicationContext.NewLink("self", selfUrl);
      i.AddLink(selfLink);

      Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = issue.Issue.OwnerProject.Id });
      Link projectLink = CommunicationContext.NewLink("up", projectUrl, "Containing project");
      i.AddLink(projectLink);

      Uri attachmentsUrl = typeof(IssueAttachmentsResource).CreateUri(new { id = issue.Issue.Id });
      Link attachmentsLink = CommunicationContext.NewLink("is:atttachments", attachmentsUrl, "All attachments for this issue");
      i.AddLink(attachmentsLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Title = issue.Issue.Title;
      updateTemplate.Description = issue.Issue.Description;
      updateTemplate.Severity = issue.Issue.Severity;

      Net.Action updateAction = CommunicationContext.NewAction("is:update-issue", MasonProperties.ActionTypes.JSON, selfUrl, "Update issue details", template: (DynamicDictionary)updateTemplate);
      i.AddAction(updateAction);

      Net.Action deleteAction = CommunicationContext.NewAction("is:delete-issue", MasonProperties.ActionTypes.Void, selfUrl, "Delete issue", method: "DELETE");
      i.AddAction(deleteAction);

      Uri addAttachmentSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-attachment" });
      Net.Action addAttachmentAction = CommunicationContext.NewAction("is:add-attachment", MasonProperties.ActionTypes.JSONFiles, attachmentsUrl, "Add new attachment to issue", schemaUrl: addAttachmentSchemaUrl);
      if (!CommunicationContext.PreferMinimalResponse())
      {
        addAttachmentAction.jsonFile = "args";
        addAttachmentAction.AddFile("attachment", "Attachment for issue");
      }
      i.AddAction(addAttachmentAction);

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
        Link attachmentLink = CommunicationContext.NewLink("self", attachmentUrl);
        a.AddLink(attachmentLink);

        attachments.Add(a);
      }

      i.Attachments = attachments;

      return i;
    }
  }
}
