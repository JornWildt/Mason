using Mason.IssueTracker.Server.Domain.Attachments;
using Mason.IssueTracker.Server.Domain.Issues;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Issues.Resources
{
  public class IssueAttachmentsResource
  {
    public Issue Issue { get; set; }
    public List<Attachment> Attachments { get; set; }
  }
}
