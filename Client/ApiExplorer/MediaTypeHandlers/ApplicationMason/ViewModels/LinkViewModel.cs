using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using Ramone;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class LinkViewModel : ElementViewModel
  {
    #region UI properties

    public string Rel { get; set; }

    public string HRef { get { return GetValue<string>("href"); } }

    public string Title { get { return GetValue<string>("title"); } }

    public string DisplayTitle1 { get; set; }

    public string DisplayTitle2 { get; set; }

    public string ToolTip { get; set; }


    public ObservableCollection<LinkViewModel> AlternateLinks { get; set; }

    #endregion


    #region Commands

    public DelegateCommand<object> FollowLinkCommand { get; private set; }

    #endregion


    public LinkViewModel(ViewModel parent, JProperty link, BuilderContext context)
      : this(parent, link.Value as JObject, link.Name, context)
    {
      JArray alt = link.Value["alt"] as JArray;
      if (alt != null)
      {
        AlternateLinks = new ObservableCollection<LinkViewModel>();
        foreach (var l1 in alt)
        {
          JObject l = l1 as JObject;
          if (l != null)
            AlternateLinks.Add(new LinkViewModel(this, l, link.Name, context));
        }
      }
    }


    public LinkViewModel(ViewModel parent, JObject link, string rel, BuilderContext context)
      : base(parent, link)
    {
      string prefix;
      string reference;
      string nsname;

      Rel = context.Namespaces.Expand(rel, out prefix, out reference, out nsname);

      ToolTip = (string.IsNullOrWhiteSpace(Title) ? "" : Title + "\n");
      ToolTip += "Links to " + HRef;

      if (reference != null && nsname != null)
      {
        DisplayTitle1 = nsname;
        DisplayTitle2 = reference;
      }
      else
      {
        DisplayTitle1 = "";
        DisplayTitle2 = Rel;
      }

      string type = GetValue<string>("type");
      if (!string.IsNullOrWhiteSpace(type))
        DisplayTitle2 += " (" + type + ")";

      RegisterCommand(FollowLinkCommand = new DelegateCommand<object>(FollowLink));
    }


    #region Commands

    private void FollowLink(object arg)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req = session.Bind(HRef).Method("GET");

      Publish(new ExecuteWebRequestEventArgs(session, req));
    }

    #endregion
  }
}
