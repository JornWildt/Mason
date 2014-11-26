using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class ActionViewModel : NavigationViewModel
  {
    public string Method { get; set; }


    public ActionViewModel(ViewModel parent, JProperty action, BuilderContext context)
      : base(parent, action.Value as JObject, action.Name, context)
    {
      string method = GetValue<string>("method");
      Method = method ?? "POST";

      DisplayTitle2 += " (JSON," + Method + ")";
    }
  }
}
