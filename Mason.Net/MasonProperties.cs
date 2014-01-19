namespace Mason.Net
{
  public static class MasonProperties
  {
    public const string MediaType = "application/mason+json";

    public const string Prefix = "@";

    public const string Namespaces = Prefix + "namespaces";
    public const string Links = Prefix + "links";
    public const string LinkTemplates = Prefix + "link-templates";
    public const string Actions = Prefix + "actions";
    public const string Meta = Prefix + "meta";
    public const string Profile = Prefix + "profile";
    public const string Error = Prefix + "error";

    public static class MetaProperties
    {
      public const string Title = Prefix + "title";
      public const string Description = Prefix + "description";
    }

    public static class ErrorProperties
    {
      public const string Id = Prefix + "id";
      public const string Code = Prefix + "code";
      public const string Message = Prefix + "message";
      public const string Messages = Prefix + "messages";
      public const string Details = Prefix + "details";
      public const string Time = Prefix + "time";
    }
  }
}
