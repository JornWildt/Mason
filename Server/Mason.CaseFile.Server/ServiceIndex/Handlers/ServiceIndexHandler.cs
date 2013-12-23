using Mason.CaseFile.Server.ServiceIndex.Resources;


namespace Mason.CaseFile.Server.ServiceIndex.Handlers
{
  public class ServiceIndexHandler
  {
    public object Get()
    {
      return new ServiceIndexResource
      {
        Title = "Service index for Case File",
        Description = "This is the ServiceIndex for interacting with Acme inc."
      };
    }
  }
}
