namespace Mason.IssueTracker.Server.Issues.Resources
{
  public class AddIssueArgs
  {
    public class AttachmentArgs
    {
      public string Title { get; set; }

      public string Description { get; set; }
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Severity { get; set; }

    public AttachmentArgs Attachment { get; set; }
  }
}
