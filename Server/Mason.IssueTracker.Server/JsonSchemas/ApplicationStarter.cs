using log4net;
using Mason.IssueTracker.Server.JsonSchemas.Codecs;
using Mason.IssueTracker.Server.JsonSchemas.Handlers;
using Mason.IssueTracker.Server.JsonSchemas.Resources;
using OpenRasta.Configuration;


namespace Mason.IssueTracker.Server.JsonSchemas
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting JsonSchemas");

      ResourceSpace.Has.ResourcesOfType<SchemaTypeResource>()
        .AtUri(UrlPaths.Schema)
        .HandledBy<SchemaHandler>()
        .TranscodedBy<JsonSchemaWriter>();
    }
  }
}
