using ApiExplorer.ViewModels;
using ApiExplorer.Windows;
using Newtonsoft.Json.Linq;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class VoidActionViewModel : ActionViewModel
  {
    public VoidActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json, context)
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
