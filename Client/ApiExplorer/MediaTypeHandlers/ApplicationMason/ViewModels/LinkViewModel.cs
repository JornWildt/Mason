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

    private string _rel;
    public string Rel
    {
      get { return _rel; }
      set
      {
        if (value != _rel)
        {
          _rel = value;
          OnPropertyChanged("Rel");
        }
      }
    }


    public string HRef { get { return GetValue<string>("href"); } }

    public string Title { get { return GetValue<string>("title"); } }

    public string DisplayTitle
    { 
      get 
      { 
        string type = GetValue<string>("type");
        if (!string.IsNullOrEmpty(type))
          type = " (" + type + ")";
        return Rel + type;
      } 
    }

    public string ToolTip
    {
      get
      {
        string type = GetValue<string>("type");
        if (!string.IsNullOrEmpty(type))
          type = " (" + type + ")";
        return (GetValue<string>("title") ?? Rel) + type;
      }
    }


    public ObservableCollection<LinkViewModel> AlternateLinks { get; set; }

    #endregion


    #region Commands

    public DelegateCommand<object> FollowLinkCommand { get; private set; }

    #endregion


    public LinkViewModel(ViewModel parent, JProperty link)
      : this(parent, link.Value as JObject, link.Name)
    {
      JArray alt = link.Value["alt"] as JArray;
      if (alt != null)
      {
        AlternateLinks = new ObservableCollection<LinkViewModel>();
        foreach (var l1 in alt)
        {
          JObject l = l1 as JObject;
          if (l != null)
            AlternateLinks.Add(new LinkViewModel(this, l, link.Name));
        }
      }
    }


    public LinkViewModel(ViewModel parent, JObject link, string name)
      : base(parent, link)
    {
      Rel = name;
      RegisterCommand(FollowLinkCommand = new DelegateCommand<object>(FollowLink));
    }


    #region Commands

    private void FollowLink(object arg)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req = session.Bind(HRef).Method("GET");

      Publish(new ExecuteWebRequestEventArgs { Request = req });
    }

    #endregion
  }
}
