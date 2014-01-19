using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using Ramone;


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
        return (GetValue<string>("title") ?? Rel) + type;
      } 
    }

    #endregion


    #region Commands

    public DelegateCommand<object> FollowLinkCommand { get; private set; }

    #endregion


    public LinkViewModel(ViewModel parent, JProperty link)
      : base(parent, link.Value)
    {
      Rel = link.Name;
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
