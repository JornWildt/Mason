using Mason.IssueTracker.Server.ResourceCommons.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;
using Mason.IssueTracker.Server.Utility;


namespace Mason.IssueTracker.Server.Codecs
{
  public abstract class IssueTrackerMasonCodec<T> : MasonCodec<T>
  {
    protected override Net.Resource ConvertToMason(T resource)
    {
      Resource r = ConvertToIssueTracker(resource);

      if (!CommunicationContext.PreferMinimalResponse())
      {
        string msg = string.Format("This application restarts in {0:m\\:ss} minutes", ApplicationLifeTimeManager.NextRestart - DateTime.Now);
        if (r.Meta == null)
          r.Meta = new SubResource();
        if (r.Meta[MasonProperties.MetaProperties.Description] == null)
          r.Meta[MasonProperties.MetaProperties.Description] = msg;
        else
          r.Meta[MasonProperties.MetaProperties.Description] += " [" + msg + "]";
      }

      Uri resourceCommonUri = typeof(ResourceCommonResource).CreateUri();
      Link resourceCommonLink = CommunicationContext.NewLink(RelTypes.ResourceCommon, resourceCommonUri, "Common information shared by all resources");
      r.AddLink(resourceCommonLink);

      //r.AddNamespace(new Net.Namespace(RelTypes.NamespaceAlias, RelTypes.Namespace));
      //r[MasonProperties.Profile] = Profiles.IssueTracker;

      return r;
    }


    protected abstract Mason.Net.Resource ConvertToIssueTracker(T resource);
  }


  public class IssueTrackerMasonCodec : IssueTrackerMasonCodec<Resource>
  {
    protected override Resource ConvertToIssueTracker(Resource resource)
    {
      return resource;
    }
  }

}
