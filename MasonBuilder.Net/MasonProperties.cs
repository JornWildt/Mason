namespace MasonBuilder.Net
{
  public static class MasonProperties
  {
    public const string MediaType = "application/vnd.mason+json";

    public const string Prefix = "@";

    public const string Namespaces = Prefix + "namespaces";
    public const string Meta = Prefix + "meta";
    public const string Profile = Prefix + "profile";
    public const string Control = Prefix + "controls";
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

    public static class ControlProperties
    {
      public const string HRef = "href";
      public const string Title = "title";
      public const string Description = "description";
      public const string Method = "method";
      public const string Files = "files";
      public const string JsonFile = "jsonFile";
    }

    public static class ControlPartProperties
    {
      public const string Name = "name";
      public const string Title = "title";
      public const string Description = "description";
      public const string Accept = "accept";
    }

    public static class EncodingTypes
    {
      public const string None = "none";
      public const string JSON = "json";
      public const string JSONFiles = "json+files";
      public const string Raw = "raw";
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
