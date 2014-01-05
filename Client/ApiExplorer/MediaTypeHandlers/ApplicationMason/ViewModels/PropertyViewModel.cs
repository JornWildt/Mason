using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class PropertyViewModel : JsonViewModel
  {
    #region UI properties

    public string Name { get; set; }

    public object Value { get; set; }

    private bool _isExpanded;
    public bool IsExpanded
    {
      get { return _isExpanded; }
      set
      {
        if (value != _isExpanded)
        {
          _isExpanded = value;
          OnPropertyChanged("IsExpanded");
        }
      }
    }

    #endregion


    public PropertyViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
      IsExpanded = true;
    }
  }


  public class ResourcePropertyViewModel : PropertyViewModel
  {
    public ResourcePropertyViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
    }
  }
}
