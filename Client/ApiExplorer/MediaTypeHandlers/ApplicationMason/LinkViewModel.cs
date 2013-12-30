using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class LinkViewModel : ViewModel
  {
    private Link Source { get; set; }

    
    #region UI properties

    public string Rel { get { return Source.rel; } }

    public string HRef { get { return Source.href; } }

    public string Title { get { return Source.title ?? Source.rel; } }

    #endregion


    #region Commands

    public DelegateCommand<object> FollowLinkCommand { get; private set; }

    #endregion


    public LinkViewModel(ViewModel parent, Link link)
      : base(parent)
    {
      Source = link;
      RegisterCommand(FollowLinkCommand = new DelegateCommand<object>(FollowLink));
    }

    
    #region Follow link

    private void FollowLink(object obj)
    {
    }

    #endregion
  }
}
