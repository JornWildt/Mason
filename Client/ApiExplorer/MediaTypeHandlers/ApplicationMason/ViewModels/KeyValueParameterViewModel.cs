using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class KeyValueParameterViewModel : JsonViewModel
  {
    #region UI properties

    private string _name;
    public string Name
    {
      get { return _name; }
      set
      {
        if (value != _name)
        {
          _name = value;
          OnPropertyChanged("Name");
        }
      }
    }


    private string _type;
    public string Type
    {
      get { return _type; }
      set
      {
        if (value != _type)
        {
          _type = value;
          OnPropertyChanged("Type");
        }
      }
    }


    private string _description;
    public string Description
    {
      get { return _description; }
      set
      {
        if (value != _description)
        {
          _description = value;
          OnPropertyChanged("Description");
        }
      }
    }


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
      Name = GetValue<string>("name");
      Type = GetValue<string>("type");
      Description = GetValue<string>("description");
      Value = GetValue<string>("value");
    }


    public KeyValueParameterViewModel(ViewModel parent, string name)
      : base(parent, new JObject())
    {
      Name = name;
    }
  }
}
