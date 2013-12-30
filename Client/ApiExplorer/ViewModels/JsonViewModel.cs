using Newtonsoft.Json.Linq;


namespace ApiExplorer.ViewModels
{
  public class JsonViewModel : ViewModel
  {
    public JsonViewModel(ViewModel parent)
      : base(parent)
    {
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
