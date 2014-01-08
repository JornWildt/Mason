using Mason.IssueTracker.Server.ServiceIndex.Resources;
using Mason.IssueTracker.Server.Utility;


namespace Mason.IssueTracker.Server.ServiceIndex.Handlers
{
  public class ServiceIndexHandler
  {
    public object Get()
    {
      return new ServiceIndexResource();
    }
  }
}
