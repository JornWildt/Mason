using Mason.IssueTracker.Server.Origin.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Codecs
{
  public abstract class IssueTrackerMasonCodec<T> : MasonCodec<T>
  {
    protected override Net.Resource ConvertToMason(T resource)
    {
      Net.Resource r = ConvertToIssueTracker(resource);

      Uri originUri = typeof(OriginResource).CreateUri();
      Link originLink = new Link(RelTypes.Origin, originUri);
      r.AddLink(originLink);

      r.AddNamespace(new Net.Namespace(RelTypes.NamespaceAlias, RelTypes.Namespace));
      r[MasonProperties.Profile] = Profiles.IssueTracker;

      return r;
    }


    protected abstract Mason.Net.Resource ConvertToIssueTracker(T resource);
  }
}
