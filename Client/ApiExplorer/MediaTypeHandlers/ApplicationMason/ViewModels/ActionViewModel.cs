using ApiExplorer.ViewModels;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class ActionViewModel : ElementViewModel
  {
    #region UI properties

    private string _name;
    public string Name
    {
      get { return _name; }
      set
      {
        if (value != _name)
        {
          _name = value;
          OnPropertyChanged("Name");
        }
      }
    }

    public string Type { get { return GetValue<string>("type"); } }

    public string HRef { get { return GetValue<string>("href"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string DisplayTitle
    {
      get
      {
        return GetValue<string>("description") ?? Name;
      }
    }

    #endregion


    #region Commands

    public DelegateCommand<object> OpenActionCommand { get; private set; }

    public DelegateCommand<object> SubmitCommand { get; private set; }

    public DelegateCommand<object> CancelCommand { get; private set; }

    #endregion


    public ActionViewModel(ViewModel parent, JProperty json)
      : base(parent, json.Value)
    {
      RegisterCommand(OpenActionCommand = new DelegateCommand<object>(OpenAction));
      RegisterCommand(SubmitCommand = new DelegateCommand<object>(Submit));
      RegisterCommand(CancelCommand = new DelegateCommand<object>(Cancel));

      Name = json.Name;
    }


    public static ActionViewModel CreateAction(ViewModel parent, JProperty action)
    {
      JToken jtype = action.Value["type"];
      string type = (jtype != null ? jtype.Value<string>() : null);

      if (type == "multipart-json")
        return new MultipartJsonActionViewModel(parent, action);
      else if (type == "json")
        return new JsonActionViewModel(parent, action);

      throw new InvalidOperationException(string.Format("Unknown action type '{0}'.", type));
    }

    
    protected void Cancel(object sender)
    {
      Window w = Window.GetWindow(sender as DependencyObject);
      w.Close();
    }

    
    protected abstract void OpenAction(object obj);

    protected abstract void Submit(object sender);
  }
}
