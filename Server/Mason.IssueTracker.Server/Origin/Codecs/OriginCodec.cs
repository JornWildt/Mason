using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Origin.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Origin.Codecs
{
  public class OriginCodec : IssueTrackerMasonCodec<OriginResource>
  {
    protected override Net.Resource ConvertToIssueTracker(OriginResource resource)
    {
      Resource o = resource.Value;

      o.SetMeta(MasonProperties.MetaProperties.Title, "Origin data for " + ((dynamic)resource).Value.Title);
      o.SetMeta(MasonProperties.MetaProperties.Description, "This resource contains origin information for issues in " + ((dynamic)resource).Value.Title);

      Uri selfUri = typeof(OriginResource).CreateUri();
      Link selfLink = new Link("self", selfUri);
      o.AddLink(selfLink);

      return o;
    }
  }
}
