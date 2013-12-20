using Mason.CaseFile.Server.Origin.Resources;


namespace Mason.CaseFile.Server.Origin.Handlers
{
  public class OriginHandler
  {
    public object Get()
    {
      return new OriginResource
      {
        Title = "Case file server v0.0",
        Introduction = "This is the origin of case files for Acme inc."
      };
    }
  }
}
