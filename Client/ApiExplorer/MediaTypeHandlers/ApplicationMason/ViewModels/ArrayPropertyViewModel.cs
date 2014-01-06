using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class ArrayPropertyViewModel : PropertyViewModel
  {
    public ArrayPropertyViewModel(ViewModel parent, JToken json, string name, object value)
      : base(parent, json, name, value)
    {
    }
  }
}
