namespace Mason.IssueTracker.Server
{
  public static class UrlPaths
  {
    public const string Projects = "projects";
    public const string Project = "projects/{id}";
    public const string ProjectIssues = "projects/{id}/issues";

    public const string Issues = "issues";
    public const string Issue = "issues/{id}";
    public const string IssueQuery = "issues-query?text={text}&severity={severity}&project={pid}";
    public const string IssueAttachments = "issues/{id}/attachments";

    public const string Attachment = "attachments/{id}";
    public const string AttachmentContent = "attachments/{id}/content";

    public const string Schema = "/schemas/{name}";
  }
}
