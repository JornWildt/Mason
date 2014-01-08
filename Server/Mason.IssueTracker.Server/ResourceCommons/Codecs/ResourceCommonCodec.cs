using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.ResourceCommons.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;


namespace Mason.IssueTracker.Server.ResourceCommons.Codecs
{
  public class ResourceCommonCodec : IssueTrackerMasonCodec<ResourceCommonResource>
  {
    protected override Net.Resource ConvertToIssueTracker(ResourceCommonResource resource)
    {
      Resource o = resource.Value;

      o.SetMeta(MasonProperties.MetaProperties.Title, "Common resource data for " + Settings.OriginName);
      o.SetMeta(MasonProperties.MetaProperties.Description, "This resource contains common information for all resources (such as implementation and owner details).");

      return o;
    }
  }
}
