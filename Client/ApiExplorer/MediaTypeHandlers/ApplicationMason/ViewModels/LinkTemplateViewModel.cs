using ApiExplorer.MediaTypeHandlers.ApplicationMason.Windows;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json.Linq;
using Ramone;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class LinkTemplateViewModel : JsonViewModel
  {
    #region UI properties

    public string Name { get { return GetValue<string>("name"); } }
    
    public string Template { get { return GetValue<string>("template"); } }

    public string Title { get { return GetValue<string>("title"); } }

    public string DisplayTitle
    {
      get
      {
        return GetValue<string>("title") ?? Name;
      }
    }


    private ObservableCollection<KeyValueParameterViewModel> _parameters;
    public ObservableCollection<KeyValueParameterViewModel> Parameters
    {
      get { return _parameters; }
      set
      {
        if (value != _parameters)
        {
          _parameters = value;
          OnPropertyChanged("Parameters");
        }
      }
    }

    #endregion




    #region Commands

    public DelegateCommand<object> OpenLinkTemplateCommand { get; private set; }

    public DelegateCommand<object> SetStatusCommand { get; private set; }

    public DelegateCommand<object> ResetStatusCommand { get; private set; }

    public DelegateCommand<object> SubmitCommand { get; private set; }

    public DelegateCommand<object> CancelCommand { get; private set; }

    #endregion


    public LinkTemplateViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
      RegisterCommand(OpenLinkTemplateCommand = new DelegateCommand<object>(OpenLinkTemplate));
      RegisterCommand(SetStatusCommand = new DelegateCommand<object>(SetStatus));
      RegisterCommand(ResetStatusCommand = new DelegateCommand<object>(ResetStatus));
      RegisterCommand(SubmitCommand = new DelegateCommand<object>(Submit));
      RegisterCommand(CancelCommand = new DelegateCommand<object>(Cancel));

      if (json == null)
        throw new ArgumentNullException("json");
      if (!(json is JObject))
        throw new InvalidOperationException("Expected JSON object for link template");

      JArray parameters = GetValue<JArray>(json, "parameters");
      if (parameters != null)
        Parameters = new ObservableCollection<KeyValueParameterViewModel>(parameters.Select(p => new KeyValueParameterViewModel(this, p)));
      else
        Parameters = new ObservableCollection<KeyValueParameterViewModel>();
    }


    #region Commands

    private void OpenLinkTemplate(object arg)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = JsonValue.ToString() });
      UrlTemplatePopupWindow w = new UrlTemplatePopupWindow(this);
      if (Parameters.Count > 0)
        Parameters[0].IsFocused = true;
      w.Owner = Window.GetWindow(arg as DependencyObject);
      w.ShowDialog();
    }


    private void SetStatus(object arg)
    {
      Publish(new SetStatusLineTextEventArgs { Text = Template });
    }


    private void ResetStatus(object arg)
    {
      Publish(new ResetStatusLineTextEventArgs());
    }


    private void Submit(object sender)
    {
      Dictionary<string, string> values = new Dictionary<string, string>();
      foreach (KeyValueParameterViewModel p in Parameters)
        values[p.Name] = p.Value;

      ISession session = RamoneServiceManager.Service.NewSession();

      Request req =
        session.Bind(Template, values)
               .Accept("application/vnd.mason;q=1, */*;q=0.5");

      Window w = Window.GetWindow(sender as DependencyObject);

      Publish(new ExecuteWebRequestEventArgs { Request = req, OnSuccess = (r => HandleSuccess(r, w)) });
    }

    
    private void HandleSuccess(Response r, Window w)
    {
      w.Close();
    }


    private void Cancel(object arg)
    {
    }

    #endregion
  }
}
