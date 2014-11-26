namespace Mason.Net
{
  public static class MasonProperties
  {
    public const string MediaType = "application/vnd.mason+json";

    public const string Prefix = "@";

    public const string Namespaces = Prefix + "namespaces";
    public const string Meta = Prefix + "meta";
    public const string Profile = Prefix + "profile";
    public const string Navigation = Prefix + "navigation";
    public const string Error = Prefix + "error";

    public static class NamespaceProperties
    {
      public const string Name = "name";
    }

    public static class MetaProperties
    {
      public const string Title = Prefix + "title";
      public const string Description = Prefix + "description";
    }

    public static class NavigationTypes
    {
      public const string Link = "link";
      public const string LinkTemplate = "link-template";
      public const string JSON = "json";
      public const string JSONFiles = "json+files";
      public const string Void = "void";
    }

    public static class ActionProperties
    {
      public const string JsonFile = "jsonFile";
      public const string Files = "files";
      public const string Files_Name = "name";
      public const string Files_Description = "description";
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
