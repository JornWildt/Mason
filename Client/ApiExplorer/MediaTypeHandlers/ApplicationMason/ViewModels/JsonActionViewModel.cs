using ApiExplorer.MediaTypeHandlers.ApplicationMason.Windows;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
//using JsonSchema;
//using JsonSchema.Parser;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Ramone;
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


    public JsonActionViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
      JToken schemaJson = json["schema"];
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
      JsonActionPopupWindow w = new JsonActionPopupWindow(this);
      w.Owner = Window.GetWindow(sender as DependencyObject);
      w.ShowDialog();
    }


    protected override void Submit(object sender)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req =
        session.Bind(HRef)
               .Accept("application/vnd.mason;q=1, */*;q=0.5")
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
