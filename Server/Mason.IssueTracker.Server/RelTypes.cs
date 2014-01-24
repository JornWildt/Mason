namespace Mason.IssueTracker.Server
{
  public static class RelTypes
  {
    public const string NSPrefix = "is";
    public const string NSName = "http://mason-issue-tracker.dk/rels#";

    public const string ResourceCommon = NSPrefix + ":common";
    public const string Contact = NSPrefix + ":contact";
    public const string Logo = NSPrefix + ":logo";

    public const string Projects = NSPrefix + ":projects";
    public const string CreateProject = NSPrefix + ":project-create";

    public const string IssueQuery = NSPrefix + ":issue-query";
    public const string CreateIssue = NSPrefix + ":issue-create";
  }
}
