using Mason.IssueTracker.Server.ResourceCommons.Resources;
using MasonBuilder.Net;
using OpenRasta.Web;
using System;
using Mason.IssueTracker.Server.Utility;


namespace Mason.IssueTracker.Server.Codecs
{
  public abstract class IssueTrackerMasonCodec<T> : MasonCodec<T>
  {
    protected override Resource ConvertToMason(T resource)
    {
      Resource r = ConvertToIssueTracker(resource);

      r.AddNamespace(new Namespace(RelTypes.NSPrefix, RelTypes.NSName));

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        string msg = string.Format("This application restarts in {0:m\\:ss} minutes", ApplicationLifeTimeManager.NextRestart - DateTime.Now);
        if (r.Meta.Description == null)
          r.Meta.Description = msg;
        else
          r.Meta.Description += " [" + msg + "]";
        r.Meta.AddControl(MasonBuilderContext.NewLink("documentation", "https://github.com/JornWildt/Mason", "Documentation of Mason media type (hosted on GitHub)."));
      }

      Uri resourceCommonUri = typeof(ResourceCommonResource).CreateUri();
      Control resourceCommonLink = MasonBuilderContext.NewLink(RelTypes.ResourceCommon, resourceCommonUri, "Common information shared by all resources");
      r.AddControl(resourceCommonLink);

      return r;
    }


    protected abstract Resource ConvertToIssueTracker(T resource);
  }


  public class IssueTrackerMasonCodec : IssueTrackerMasonCodec<Resource>
  {
    protected override Resource ConvertToIssueTracker(Resource resource)
    {
      return resource;
    }
  }

}
