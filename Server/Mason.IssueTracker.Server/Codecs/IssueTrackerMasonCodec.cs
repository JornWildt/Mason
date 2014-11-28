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

      r.AddNamespace(new Namespace(RelTypes.NSPrefix, RelTypes.NSName));

      if (!CommunicationContext.PreferMinimalResponse())
      {
        string msg = string.Format("This application restarts in {0:m\\:ss} minutes", ApplicationLifeTimeManager.NextRestart - DateTime.Now);
        if (r.Meta.Description == null)
          r.Meta.Description = msg;
        else
          r.Meta.Description += " [" + msg + "]";
        r.Meta.AddNavigation(CommunicationContext.NewLink("documentation", "https://github.com/JornWildt/Mason", "Documentation of Mason media type (hosted on GitHub)."));
      }

      Uri resourceCommonUri = typeof(ResourceCommonResource).CreateUri();
      Link resourceCommonLink = CommunicationContext.NewLink(RelTypes.ResourceCommon, resourceCommonUri, "Common information shared by all resources");
      r.AddNavigation(resourceCommonLink);

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
