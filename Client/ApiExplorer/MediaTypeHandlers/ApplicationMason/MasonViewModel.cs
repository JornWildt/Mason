using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonViewModel : ViewModel
  {
    #region Sub-viewmodels

    public ResourceViewModel RootResource { get; set; }

    #endregion


    public MasonViewModel(ViewModel parent, JObject resource)
      : base(parent)
    {
      RootResource = new ResourceViewModel(parent, resource);
      //if (resource.Meta != null && resource.Meta[MasonProperties.Title] is string)
      //  Publish(new TitleChangedEventArgs { Title = (string)resource.Meta["mason:title"] });
    }
  }
}
