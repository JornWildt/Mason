namespace Mason.IssueTracker.Server
{
  public static class RelTypes
  {
    public const string NSPrefix = "is";
    public const string NSName = "http://soabits.dk/mason/issue-tracker/reltypes.html#";

    public const string ResourceCommon = NSPrefix + ":common";
    public const string Contact = NSPrefix + ":contact";
    public const string Logo = NSPrefix + ":logo";

    public const string Projects = NSPrefix + ":projects";
    public const string ProjectAdd = NSPrefix + ":project-create";
    public const string ProjectUpdate = NSPrefix + ":project-update";
    public const string ProjectDelete = NSPrefix + ":project-delete";
    public const string ProjectAddIssue = NSPrefix + ":add-issue";

    public const string Issues = NSPrefix + ":issues";
    public const string IssueAdd = NSPrefix + ":issue-create";
    public const string IssueUpdate = NSPrefix + ":issue-update";
    public const string IssueDelete = NSPrefix + ":issue-delete";
    public const string IssueAddAttachment = NSPrefix + ":add-attachment";
    public const string IssueQuery = NSPrefix + ":issue-query";

    public const string Attachments = NSPrefix + ":attachments";
    public const string AttachmentContent = NSPrefix + ":content";
    public const string AttachmentUpdate = NSPrefix + ":update-attachment";
    public const string AttachmentDelete = NSPrefix + ":delete-attachment";
  }
}
