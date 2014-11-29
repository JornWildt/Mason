using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class ArrayPropertyViewModel : PropertyViewModel
  {
    public int Count
    {
      get { return ((ObservableCollection<ViewModel>)Value).Count; }
    }


    public ArrayPropertyViewModel(ViewModel parent, JToken json, string name, object value)
      : base(parent, json, name, value)
    {
    }
  }
}
