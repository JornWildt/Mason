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

    public string Title { get { return GetValue<string>(Source, "title") ?? Rel; } }

    #endregion


    #region Commands

    public DelegateCommand<object> FollowLinkCommand { get; private set; }

    #endregion


    public LinkViewModel(ViewModel parent, JObject link)
      : base(parent)
    {
      Source = link;
      RegisterCommand(FollowLinkCommand = new DelegateCommand<object>(FollowLink));
    }

    
    #region Follow link

    private void FollowLink(object obj)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req =
        session.Bind(HRef)
               .Accept("application/vnd.mason;q=1, */*;q=0.5");

      Publish(new ExecuteWebRequestEventArgs { Request = req });
    }

    #endregion
  }
}
