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

    #endregion
  }
}
