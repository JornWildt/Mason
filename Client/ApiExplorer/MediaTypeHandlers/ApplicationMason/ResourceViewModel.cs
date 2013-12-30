using ApiExplorer.ViewModels;
using Mason.Net;
using System.Collections.ObjectModel;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class ResourceViewModel : ViewModel
  {
    #region UI properties

    public ObservableCollection<LinkViewModel> Links { get; private set; }

    public ObservableCollection<PropertyViewModel> Properties { get; private set; }

    #endregion


    public ResourceViewModel(ViewModel parent, Resource resource)
      : base(parent)
    {
      Links = new ObservableCollection<LinkViewModel>(
        resource.Links == null
          ? Enumerable.Empty<LinkViewModel>()
          : resource.Links.Select(l => new LinkViewModel(this, l)));

      Properties = new ObservableCollection<PropertyViewModel>(
        resource.GetDynamicMemberNames().Select(name => new PropertyViewModel(this) { Name = name, Value = (resource[name] != null ? resource[name].ToString() : null)  }));
    }
  }
}
