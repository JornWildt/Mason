using ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs;
using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class MultipartJsonActionViewModel : ActionViewModel
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


    public MultipartJsonActionViewModel(ViewModel parent, JProperty json)
      : base(parent, json)
    {
      JsonText = "{\nNOT IMPLEMENTED YET\n}";
    }


    #region Commands

    protected override void OpenAction(object sender)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = JsonValue.ToString() });
      MultipartJsonActionPopupDialog w = new MultipartJsonActionPopupDialog(this);
      w.Owner = Window.GetWindow(sender as DependencyObject);
      w.ShowDialog();
    }


    protected override void Submit(object sender)
    {
      //Dictionary<string, string> values = new Dictionary<string, string>();
      //foreach (KeyValueParameterViewModel p in Parameters)
      //  values[p.Name] = p.Value;

      //ISession session = RamoneServiceManager.Service.NewSession();

      //Request req =
      //  session.Bind(Template, values)
      //         .Accept("application/vnd.mason;q=1, */*;q=0.5");

      //Window w = Window.GetWindow(sender as DependencyObject);

      //Publish(new ExecuteWebRequestEventArgs { Request = req, OnSuccess = (r => HandleSuccess(r, w)) });
    }


    //private void HandleSuccess(Response r, Window w)
    //{
    //  w.Close();
    //}

    #endregion
  }
}
