namespace Mason.Net
{
  public static class MasonProperties
  {
    public const string Namespaces = "mason:namespaces";
    public const string Links = "mason:links";
    public const string LinkTemplates = "mason:link-templates";
    public const string Actions = "mason:actions";
    public const string Meta = "mason:meta";
    public const string Profile = "mason:profile";
    public const string Error = "mason:error";

    public static class MetaProperties
    {
      public const string Title = "mason:title";
      public const string Description = "mason:description";
    }

    public static class ErrorProperties
    {
      public const string Id = "mason:id";
      public const string Code = "mason:code";
      public const string Message = "mason:message";
      public const string Messages = "mason:messages";
      public const string Details = "mason:details";
      public const string Time = "mason:time";
    }
  }
}
