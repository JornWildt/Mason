using ApiExplorer.ViewModels;
using CuttingEdge.Conditions;
using Newtonsoft.Json.Linq;
using System;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class ActionViewModel : JsonViewModel
  {
    #region UI properties

    public string Name { get { return GetValue<string>("name"); } }

    public string Type { get { return GetValue<string>("type"); } }

    public string HRef { get { return GetValue<string>("href"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string DisplayTitle
    {
      get
      {
        return GetValue<string>("description") ?? Name;
      }
    }

    #endregion


    public ActionViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
    }


    public static ActionViewModel CreateAction(ViewModel parent, JToken json)
    {
      Condition.Requires(json, "json").IsNotNull();

      JToken jtype = json["type"];
      string type = (jtype != null ? jtype.Value<string>() : null);

      if (type == "multipart-json")
        return new MultipartJsonActionViewModel(parent, json);

      throw new InvalidOperationException(string.Format("Unknown action type '{0}'.", type));
    }
  }
}
