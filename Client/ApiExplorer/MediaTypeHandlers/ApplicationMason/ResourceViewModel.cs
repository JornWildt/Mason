using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class ResourceViewModel : JsonViewModel
  {
    #region UI properties

    public ObservableCollection<LinkViewModel> Links { get; private set; }

    public ObservableCollection<PropertyViewModel> Properties { get; private set; }

    #endregion


    public ResourceViewModel(ViewModel parent, JObject resource)
      : base(parent)
    {
      Properties = new ObservableCollection<PropertyViewModel>();

      foreach (var pair in resource)
      {
        if (pair.Key == MasonProperties.Links && pair.Value is JArray)
        {
          Links = new ObservableCollection<LinkViewModel>(
            pair.Value.Children().OfType<JObject>().Select(l => new LinkViewModel(this, l)));
        }
        else
        {
          if (!(pair.Value is JObject))
            Properties.Add(new PropertyViewModel(this) { Name = pair.Key, Value = (pair.Value != null ? pair.Value.ToString() : "") });
        }
      }

      if (Links == null)
        Links = new ObservableCollection<LinkViewModel>();


      //  resource.Links == null
      //    ? Enumerable.Empty<LinkViewModel>()
      //    : resource.Links.Select(l => new LinkViewModel(this, l)));

      //Properties = new ObservableCollection<PropertyViewModel>(
      //  resource.GetDynamicMemberNames().Select(name => new PropertyViewModel(this) { Name = name, Value = (resource[name] != null ? resource[name].ToString() : null)  }));
    }
  }
}
