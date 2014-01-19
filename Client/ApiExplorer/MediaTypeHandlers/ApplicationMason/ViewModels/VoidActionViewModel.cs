using ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
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

      VoidActionPopupDialog d = new VoidActionPopupDialog(this);
      d.Owner = Window.GetWindow(sender as DependencyObject);
      d.ShowDialog();
    }


    protected override void Submit(object sender)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req =
        session.Bind(HRef)
               //.Accept("application/vnd.mason;q=1, */*;q=0.5")
               //.AsJson()
               //.Body(JsonText)
               .Method(Method);

      Window w = Window.GetWindow(sender as DependencyObject);

      Publish(new ExecuteWebRequestEventArgs { Request = req, OnSuccess = (r => HandleSuccess(r, w)) });
    }


    private void HandleSuccess(Response r, Window w)
    {
      w.Close();
    }
  }
}
