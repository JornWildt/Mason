using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class KeyValueParameterViewModel : JsonViewModel
  {
    #region UI properties

    public string Name { get { return GetValue<string>("name"); } }

    public string Type { get { return GetValue<string>("type"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string DisplayTitle
    {
      get
      {
        return Name;
      }
    }


    private string _value;
    public string Value
    {
      get { return _value; }
      set
      {
        if (value != _value)
        {
          _value = value;
          OnPropertyChanged("Value");
        }
      }
    }


    private bool _isFocused;
    public bool IsFocused
    {
      get { return _isFocused; }
      set
      {
        if (value != _isFocused)
        {
          _isFocused = value;
          OnPropertyChanged("IsFocused");
        }
      }
    }



    #endregion


    public KeyValueParameterViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
      Value = GetValue<string>("value");
    }
  }
}
