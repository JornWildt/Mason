using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using MasonBuilder.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using Ramone;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class LinkViewModel : ControlViewModel
  {
    public override string ControlType { get { return MasonProperties.ControlTypes.Link; } }


    public ObservableCollection<LinkViewModel> AlternateLinks { get; set; }


    public LinkViewModel(ViewModel parent, JProperty link, BuilderContext context)
      : base(parent, link.Value as JObject, link.Name, context)
    {
      JArray alt = link.Value["alt"] as JArray;
      if (alt != null)
      {
        AlternateLinks = new ObservableCollection<LinkViewModel>();
        for (int i=0; i<alt.Count; ++i)
        {
          JObject l = alt[i] as JObject;
          if (l != null)
            AlternateLinks.Add(new LinkViewModel(this, l, string.Format("alt[{0}]",i), context));
        }
      }
    }


    public LinkViewModel(ViewModel parent, JObject link, string name, BuilderContext context)
      : base(parent, link, name, context)
    {
    }


    #region Commands

    protected override void ActivateControl(object args)
    {
      ISession session = RamoneServiceManager.Session;

      Request req = session.Bind(HRef).Method("GET");

      Publish(new ExecuteWebRequestEventArgs(session, req));
    }

    #endregion
  }
}
