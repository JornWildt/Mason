using ApiExplorer.ViewModels;
using ApiExplorer.Windows;
using Newtonsoft.Json.Linq;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class JsonActionViewModel : ControlViewModel
  {
    public override string ControlType { get { return "JSON"; } }


    public JsonActionViewModel(ViewModel parent, string name, JObject json, BuilderContext context, IControlBuilder cb)
      : base(parent, name, json, context, cb)
    {
    }


    #region Commands

    protected override void ActivateControl(object sender)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = OriginalJsonValue.ToString() });

      string jsonText = CalculateJsonPayload();
      Window w = Window.GetWindow(sender as DependencyObject);
      ComposerWindow.OpenComposerWindow(w, this, Method, HRef, IsHRefTemplate, body: jsonText, actionType: Encoding, title: Title, description: Description, modifier: ModifyComposerWindow, focus: ComposerWindow.StartFocus.Body);
    }

    #endregion

    protected virtual void ModifyComposerWindow(ComposerViewModel vm)
    {
    }
  }
}
