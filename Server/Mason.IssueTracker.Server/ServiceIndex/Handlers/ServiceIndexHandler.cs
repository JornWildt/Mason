using Mason.IssueTracker.Server.ServiceIndex.Resources;


namespace Mason.IssueTracker.Server.ServiceIndex.Handlers
{
  public class ServiceIndexHandler
  {
    public object Get()
    {
      return new ServiceIndexResource
      {
        Title = "Issue tracker service index",
        Description = "This is the service index for issue tracker."
      };
    }
  }
}
