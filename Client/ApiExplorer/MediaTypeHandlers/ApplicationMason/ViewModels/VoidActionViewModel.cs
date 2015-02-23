using ApiExplorer.ViewModels;
using ApiExplorer.Windows;
using MasonBuilder.Net;
using Newtonsoft.Json.Linq;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class VoidActionViewModel : ControlViewModel
  {
    public override string ControlType { get { return IsHRefTemplate ? "Link template" : "Action"; } }


    public VoidActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json, context)
    {
    }

    protected override void ActivateControl(object sender)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = OriginalJsonValue.ToString() });

      string jsonText = CalculateJsonPayload();

      Window w = Window.GetWindow(sender as DependencyObject);
      ComposerWindow.OpenComposerWindow(w, this, Method, HRef, IsHRefTemplate, body: jsonText, title: Title, description: Description, actionType: Serialization);
    }
  }
}
