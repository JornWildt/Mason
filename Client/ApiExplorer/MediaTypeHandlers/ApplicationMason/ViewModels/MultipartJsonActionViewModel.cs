using ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs;
using ApiExplorer.ViewModels;
using ApiExplorer.Windows;
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

      Window w = Window.GetWindow(sender as DependencyObject);
      string title = Description ?? "JSON+Files Action";
      ComposerWindow.OpenComposerWindow(w, this, Method, HRef, title, JsonText, "multipart/forms");
    }

    #endregion
  }
}
