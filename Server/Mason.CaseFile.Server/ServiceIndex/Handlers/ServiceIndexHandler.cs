using Mason.CaseFile.Server.ServiceIndex.Resources;


namespace Mason.CaseFile.Server.ServiceIndex.Handlers
{
  public class ServiceIndexHandler
  {
    public object Get()
    {
      return new ServiceIndexResource
      {
        Title = "Case file API service index",
        Description = "This is the API service index for case files at the Ministry of Fun"
      };
    }
  }
}
