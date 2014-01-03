using Newtonsoft.Json.Linq;


namespace ApiExplorer.ViewModels
{
  public class JsonViewModel : ViewModel
  {
    public JToken JsonValue { get; protected set; }


    public JsonViewModel(ViewModel parent, JToken jsonValue)
      : base(parent)
    {
      JsonValue = jsonValue;
    }


    protected T GetValue<T>(JToken t, string name)
      where T : class
    {
      JToken value = t[name];
      if (value != null)
        return value.Value<T>();
      return null;
    }
  }
}
