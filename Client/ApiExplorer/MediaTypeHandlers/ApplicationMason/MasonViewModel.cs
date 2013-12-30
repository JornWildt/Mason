using ApiExplorer.ViewModels;
using Mason.Net;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonViewModel : ViewModel
  {
    public string SourceText { get; set; }


    #region Sub-viewmodels

    public ResourceViewModel RootResource { get; set; }

    #endregion


    public MasonViewModel(ViewModel parent, Resource resource)
      : base(parent)
    {
      SourceText = "Not here";
      RootResource = new ResourceViewModel(parent, resource);
      if (resource.Meta != null && resource.Meta[MasonProperties.Title] is string)
        Publish(new TitleChangedEventArgs { Title = (string)resource.Meta["mason:title"] });
    }
  }
}
