using ApiExplorer.ViewModels;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class ElementViewModel : JsonViewModel
  {
    #region Commands

    public DelegateCommand<object> SetStatusCommand { get; private set; }

    public DelegateCommand<object> ResetStatusCommand { get; private set; }

    #endregion


    public ElementViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
      RegisterCommand(SetStatusCommand = new DelegateCommand<object>(SetStatus));
      RegisterCommand(ResetStatusCommand = new DelegateCommand<object>(ResetStatus));
    }


    protected void SetStatus(object arg)
    {
      Publish(new SetStatusLineTextEventArgs { Text = (arg != null ? arg.ToString() : "") });
    }


    protected void ResetStatus(object arg)
    {
      Publish(new ResetStatusLineTextEventArgs());
    }
  }
}
