using Newtonsoft.Json.Linq;


namespace ApiExplorer.ViewModels
{
  public class JsonViewModel : ViewModel
  {
    public JToken OriginalJsonValue { get; protected set; }


    public JsonViewModel(ViewModel parent, JToken jsonValue)
      : base(parent)
    {
      OriginalJsonValue = jsonValue;
    }


    protected T GetValue<T>(string name)
    {
      return GetValue<T>(OriginalJsonValue, name);
    }


    protected T GetValue<T>(string name, T defaultValue)
    {
      return GetValue<T>(OriginalJsonValue, name, defaultValue);
    }


    protected T GetValue<T>(JToken t, string name)
    {
      return GetValue<T>(t, name, default(T));
    }


    protected T GetValue<T>(JToken t, string name, T defaultValue)
    {
      JToken value = t[name];
      if (value != null)
        return value.Value<T>();
      return defaultValue;
    }
  }
}
