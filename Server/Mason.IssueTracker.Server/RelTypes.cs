namespace Mason.IssueTracker.Server
{
  public static class RelTypes
  {
    public const string NamespaceAlias = "is";
    public const string Namespace = "http://mason-issue-tracker.dk/rels/";

    public const string ResourceCommon = NamespaceAlias + ":common";
    public const string Contact = NamespaceAlias + ":contact";
    public const string Logo = NamespaceAlias + ":logo";

    public const string Projects = NamespaceAlias + ":projects";
    public const string CreateProject = NamespaceAlias + ":project-create";

    public const string IssueQuery = NamespaceAlias + ":issue-query";
    public const string CreateIssue = NamespaceAlias + ":issue-create";
  }
}
