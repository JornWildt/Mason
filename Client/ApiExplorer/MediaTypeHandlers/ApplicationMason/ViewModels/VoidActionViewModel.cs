using ApiExplorer.ViewModels;
using ApiExplorer.Windows;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class VoidActionViewModel : ActionViewModel
  {
    public override string NavigationTypeTitle { get { return "Void action"; } }


    public VoidActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json, context)
    {
    }

    protected override void ActivateNavigation(object sender)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = OriginalJsonValue.ToString() });

      Window w = Window.GetWindow(sender as DependencyObject);
      string title = (string.IsNullOrWhiteSpace(Title) ? "Void Action" : Title);
      ComposerWindow.OpenComposerWindow(w, this, Method, HRef, title, description: Description, actionType: MasonProperties.NavigationTypes.Void);
    }
  }
}
