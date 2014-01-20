using ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using ApiExplorer.Windows;
using Newtonsoft.Json.Linq;
using Ramone;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class VoidActionViewModel : ActionViewModel
  {
    public VoidActionViewModel(ViewModel parent, JProperty json)
      : base(parent, json)
    {
    }

    protected override void OpenAction(object sender)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = JsonValue.ToString() });

      Window w = Window.GetWindow(sender as DependencyObject);
      string title = Description ?? "JSON Action";
      ComposerWindow.OpenComposerWindow(w, this, Method, HRef, title);
    }
  }
}
