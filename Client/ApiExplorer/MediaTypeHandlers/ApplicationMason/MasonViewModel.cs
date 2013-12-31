using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonViewModel : ViewModel
  {
    #region Sub-viewmodels

    public ObservableCollection<ResourcePropertyViewModel> RootProperty { get; set; }

    #endregion


    public MasonViewModel(ViewModel parent, JObject resource)
      : base(parent)
    {
      // Wrap resource in collection for easier binding in TreeView
      RootProperty = new ObservableCollection<ResourcePropertyViewModel> { new ResourcePropertyViewModel(this) { Name = "ROOT RESOURCE", Value = new ResourceViewModel(this, resource) } };

      if (resource[MasonProperties.Meta] != null && resource[MasonProperties.Meta][MasonProperties.MetaProperties.Title] != null)
      {
        string title = resource[MasonProperties.Meta][MasonProperties.MetaProperties.Title].Value<string>();
        if (!string.IsNullOrEmpty(title))
        {
          RootProperty[0].Name = title;
          Publish(new TitleChangedEventArgs { Title = title });
        }
      }
    }
  }
}
