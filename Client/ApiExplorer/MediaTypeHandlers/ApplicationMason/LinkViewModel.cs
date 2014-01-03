using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using Ramone;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class LinkViewModel : JsonViewModel
  {
    private JObject Source { get; set; }

    
    #region UI properties

    public string Rel { get { return GetValue<string>(Source, "rel"); } }

    public string HRef { get { return GetValue<string>(Source, "href"); } }

    public string Title 
    { 
      get 
      { 
        string type = GetValue<string>(Source, "type");
        if (!string.IsNullOrEmpty(type))
          type = " (" + type + ")";
        return (GetValue<string>(Source, "title") ?? Rel) + type;
      } 
    }

    #endregion


    #region Commands

    public DelegateCommand<object> FollowLinkCommand { get; private set; }

    public DelegateCommand<object> SetStatusCommand { get; private set; }

    public DelegateCommand<object> ResetStatusCommand { get; private set; }

    #endregion


    public LinkViewModel(ViewModel parent, JObject link)
      : base(parent, link)
    {
      Source = link;
      RegisterCommand(FollowLinkCommand = new DelegateCommand<object>(FollowLink));
      RegisterCommand(SetStatusCommand = new DelegateCommand<object>(SetStatus));
      RegisterCommand(ResetStatusCommand = new DelegateCommand<object>(ResetStatus));
    }

    
    #region Commands

    private void FollowLink(object arg)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req =
        session.Bind(HRef)
               .Accept("application/vnd.mason;q=1, */*;q=0.5");

      Publish(new ExecuteWebRequestEventArgs { Request = req });
    }


    private void SetStatus(object arg)
    {
      Publish(new SetStatusLineTextEventArgs { Text = HRef });
    }


    private void ResetStatus(object arg)
    {
      Publish(new ResetStatusLineTextEventArgs());
    }

    #endregion
  }
}
