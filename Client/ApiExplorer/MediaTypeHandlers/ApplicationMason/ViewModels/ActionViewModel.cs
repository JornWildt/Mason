using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class ActionViewModel : ElementViewModel
  {
    #region UI properties

    public string Name { get; set; }
    
    public string Method { get; set; }

    public string Type { get { return GetValue<string>("type"); } }

    public string HRef { get { return GetValue<string>("href"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string DisplayTitle1 { get; set; }

    public string DisplayTitle2 { get; set; }

    public string ToolTip { get; set; }

    #endregion


    #region Commands

    public DelegateCommand<object> OpenActionCommand { get; private set; }

    #endregion


    public ActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json.Value)
    {
      RegisterCommand(OpenActionCommand = new DelegateCommand<object>(OpenAction));

      string prefix;
      string reference;
      string nsname;

      Name = context.Namespaces.Expand(json.Name, out prefix, out reference, out nsname);

      ToolTip = (string.IsNullOrWhiteSpace(Description) ? "" : Description + "\n");
      ToolTip += "Goes to " + HRef;

      if (reference != null && nsname != null)
      {
        DisplayTitle1 = nsname;
        DisplayTitle2 = reference;
      }
      else
      {
        DisplayTitle1 = "";
        DisplayTitle2 = Name;
      }

      string method = GetValue<string>("method");
      Method = method ?? "POST";

      DisplayTitle2 += " (" + Type + "," + Method + ")";
    }


    public static ActionViewModel CreateAction(ViewModel parent, JProperty action, BuilderContext context)
    {
      JToken jtype = action.Value["type"];
      string type = (jtype != null ? jtype.Value<string>() : null);

      if (type == MasonProperties.ActionTypes.JSON)
        return new JsonActionViewModel(parent, action, context);
      else if (type == MasonProperties.ActionTypes.Void)
        return new VoidActionViewModel(parent, action, context);
      else if (type == MasonProperties.ActionTypes.JSONFiles)
        return new MultipartJsonActionViewModel(parent, action, context);

      throw new InvalidOperationException(string.Format("Unknown action type '{0}'.", type));
    }

    
    protected abstract void OpenAction(object obj);
  }
}
