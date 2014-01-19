using ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Ramone;
using System;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class JsonActionViewModel : ActionViewModel
  {
    #region UI properties

    private string _jsonText;
    public string JsonText
    {
      get { return _jsonText; }
      set
      {
        if (value != _jsonText)
        {
          _jsonText = value;
          OnPropertyChanged("JsonText");
        }
      }
    }

    #endregion


    public JsonActionViewModel(ViewModel parent, JProperty action)
      : base(parent, action)
    {
      JToken schemaJson = action.Value["schema"];
      if (schemaJson != null)
      {
        JsonSchema schema = JsonSchema.Parse(schemaJson.Value<string>());
        JsonExampleGenerator generator = new JsonExampleGenerator();
        JsonText = generator.GenerateJsonInstanceFromSchema(schema);
      }
      else
      {
        JsonText = "{\n\n}";
      }
    }


    #region Commands

    protected override void OpenAction(object sender)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = JsonValue.ToString() });

      JToken templateJson = JsonValue["template"];

      JToken schemaUrlJson = JsonValue["schemaUrl"];
      string schemaUrl = (schemaUrlJson != null ? schemaUrlJson.Value<string>() : null);

      if (templateJson != null)
      {
        JsonText = templateJson.ToString();
      }
      else if (schemaUrlJson != null && !string.IsNullOrEmpty(schemaUrl))
      {
        try
        {
          using (var response = RamoneServiceManager.Service.NewSession().Bind(schemaUrl).Get<string>())
          {
            string schemaS = response.Body;
            JsonSchema schema = JsonSchema.Parse(schemaS);
            JsonExampleGenerator generator = new JsonExampleGenerator();
            JsonText = generator.GenerateJsonInstanceFromSchema(schema);
          }
        }
        catch (Exception ex)
        {
          JsonText = string.Format("Failed to retrieve JSON schema from '{0}': {1}", schemaUrl, ex.Message);
        }
      }

      JsonActionPopupDialog d = new JsonActionPopupDialog(this);
      d.Owner = Window.GetWindow(sender as DependencyObject);
      d.ShowDialog();
    }


    protected override void Submit(object sender)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req =
        session.Bind(HRef)
               //.Accept("application/vnd.mason;q=1, */*;q=0.5")
               .AsJson()
               .Body(JsonText)
               .Method("POST");

      Window w = Window.GetWindow(sender as DependencyObject);

      Publish(new ExecuteWebRequestEventArgs { Request = req, OnSuccess = (r => HandleSuccess(r, w)) });
    }


    private void HandleSuccess(Response r, Window w)
    {
      w.Close();
    }

    #endregion
  }
}
