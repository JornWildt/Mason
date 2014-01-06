using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class ResourcePropertyViewModel : PropertyViewModel
  {
    public ResourcePropertyViewModel(ViewModel parent, JToken json, string name, object value)
      : base(parent, json, name, value)
    {
    }
  }
}
