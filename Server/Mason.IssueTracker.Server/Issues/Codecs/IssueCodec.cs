﻿using Mason.IssueTracker.Server.Attachments.Resources;
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
      Control selfLink = MasonBuilderContext.NewLink("self", selfUrl);
      i.AddControl(selfLink);

      Uri projectUrl = typeof(ProjectResource).CreateUri(new { id = issue.Issue.OwnerProject.Id });
      Control projectLink = MasonBuilderContext.NewLink("up", projectUrl, "Containing project");
      i.AddControl(projectLink);

      Uri attachmentsUrl = typeof(IssueAttachmentsResource).CreateUri(new { id = issue.Issue.Id });
      Control attachmentsLink = MasonBuilderContext.NewLink(RelTypes.Attachments, attachmentsUrl, "All attachments for this issue");
      i.AddControl(attachmentsLink);

      dynamic updateTemplate = new DynamicDictionary();
      updateTemplate.Title = issue.Issue.Title;
      updateTemplate.Description = issue.Issue.Description;
      updateTemplate.Severity = issue.Issue.Severity;

      Control updateAction = MasonBuilderContext.NewJsonAction(RelTypes.IssueUpdate, selfUrl, "Update issue details", template: (DynamicDictionary)updateTemplate);
      i.AddControl(updateAction);

      Control deleteAction = MasonBuilderContext.NewVoidAction(RelTypes.IssueDelete, selfUrl, "Delete issue", method: "DELETE");
      i.AddControl(deleteAction);

      Uri addAttachmentSchemaUrl = typeof(SchemaTypeResource).CreateUri(new { name = "create-attachment" });
      Control addAttachmentAction = MasonBuilderContext.NewJsonFilesAction(RelTypes.IssueAddAttachment, attachmentsUrl, "args", "Add new attachment to issue", schemaUrl: addAttachmentSchemaUrl);
      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        addAttachmentAction.AddFile(new FileDefinition { name = "attachment", title = "Attachment for issue" });
      }
      i.AddControl(addAttachmentAction);

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
        Control attachmentLink = MasonBuilderContext.NewLink("self", attachmentUrl);
        a.AddControl(attachmentLink);

        Uri contentUrl = typeof(AttachmentContentResource).CreateUri(new { id = att.Id });
        Control contentLink = MasonBuilderContext.NewLink(RelTypes.AttachmentContent, contentUrl, "Download content", att.ContentType);
        a.AddControl(contentLink);

        attachments.Add(a);
      }

      ((dynamic)i).Attachments = attachments;

      return i;
    }
  }
}
