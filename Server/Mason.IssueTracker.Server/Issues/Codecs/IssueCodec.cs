using Mason.IssueTracker.Server.Attachments.Resources;
using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Issues.Resources;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using Mason.IssueTracker.Server.Projects.Resources;
using MasonBuilder.Net;
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
      Resource i = new Resource();

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        i.Meta.Title = "Issue";
        i.Meta.Description = "This resource represents a single issue with its data and related actions.";
      }

      Uri selfUrl = typeof(IssueResource).CreateUri(new { id = issue.Issue.Id });
      Link selfLink = MasonBuilderContext.NewLink("self", selfUrl);
      i.AddNavigation(selfLink);

      Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = issue.Issue.OwnerProject.Id });
      Link projectLink = MasonBuilderContext.NewLink("up", projectUrl, "Containing project");
      i.AddNavigation(projectLink);

      Uri attachmentsUrl = typeof(IssueAttachmentsResource).CreateUri(new { id = issue.Issue.Id });
      Link attachmentsLink = MasonBuilderContext.NewLink(RelTypes.Attachments, attachmentsUrl, "All attachments for this issue");
      i.AddNavigation(attachmentsLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Title = issue.Issue.Title;
      updateTemplate.Description = issue.Issue.Description;
      updateTemplate.Severity = issue.Issue.Severity;

      JsonAction updateAction = MasonBuilderContext.NewJsonAction(RelTypes.IssueUpdate, selfUrl, "Update issue details", template: (DynamicDictionary)updateTemplate);
      i.AddNavigation(updateAction);

      JsonAction deleteAction = MasonBuilderContext.NewJsonAction(RelTypes.IssueDelete, selfUrl, "Delete issue", method: "DELETE");
      i.AddNavigation(deleteAction);

      Uri addAttachmentSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-attachment" });
      JsonFilesAction addAttachmentAction = MasonBuilderContext.NewJsonFilesAction(RelTypes.IssueAddAttachment, attachmentsUrl, "Add new attachment to issue", schemaUrl: addAttachmentSchemaUrl);
      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        addAttachmentAction.jsonFile = "args";
        addAttachmentAction.AddFile("attachment", "Attachment for issue");
      }
      i.AddNavigation(addAttachmentAction);

      ((dynamic)i).ID = issue.Issue.Id;
      ((dynamic)i).Title = issue.Issue.Title;
      ((dynamic)i).Description = issue.Issue.Description;
      ((dynamic)i).Severity = issue.Issue.Severity;

      List<SubResource> attachments = new List<SubResource>();
      foreach (Attachment att in issue.Attachments)
      {
        dynamic a = new SubResource();
        a.Id = att.Id;
        a.Title = att.Title;

        Uri attachmentUrl = typeof(AttachmentResource).CreateUri(new { id = att.Id });
        Link attachmentLink = MasonBuilderContext.NewLink("self", attachmentUrl);
        a.AddNavigation(attachmentLink);

        attachments.Add(a);
      }

      ((dynamic)i).Attachments = attachments;

      return i;
    }
  }
}
