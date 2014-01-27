using ApiExplorer.MediaTypeHandlers.ApplicationMason.Dialogs;
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
  public class LinkTemplateViewModel : ElementViewModel
  {
    #region UI properties

    public string Name { get; set; }
    
    public string Template { get { return GetValue<string>("template"); } }

    public string Title { get { return GetValue<string>("title"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string ToolTip { get; set; }

    public string DisplayTitle1 { get; set; }

    public string DisplayTitle2 { get; set; }

    public string WindowTitle { get; set; }


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

    public DelegateCommand<object> SubmitCommand { get; private set; }

    public DelegateCommand<object> CancelCommand { get; private set; }

    #endregion


    public LinkTemplateViewModel(ViewModel parent, JProperty template, BuilderContext context)
      : base(parent, template.Value)
    {
      RegisterCommand(OpenLinkTemplateCommand = new DelegateCommand<object>(OpenLinkTemplate));
      RegisterCommand(SubmitCommand = new DelegateCommand<object>(Submit));
      RegisterCommand(CancelCommand = new DelegateCommand<object>(Cancel));

      if (template == null)
        throw new ArgumentNullException("json");
      if (!(template.Value is JObject))
        throw new InvalidOperationException("Expected JSON object for link template");

      string prefix;
      string reference;
      string nsname;

      Name = context.Namespaces.Expand(template.Name, out prefix, out reference, out nsname);

      ToolTip = (string.IsNullOrWhiteSpace(Title) ? "" : Title + "\n");
      ToolTip += "Links to " + Template;

      if (reference != null && nsname != null)
      {
        DisplayTitle1 = nsname;
        DisplayTitle2 = reference;
      }
      else
      {
        DisplayTitle1 = "";
        DisplayTitle2 = Name;
      }

      WindowTitle = (string.IsNullOrWhiteSpace(Title) ? "Link template editor" : Title);

      JArray parameters = GetValue<JArray>(template.Value, "parameters");
      if (parameters != null)
        Parameters = new ObservableCollection<KeyValueParameterViewModel>(parameters.Select(p => new KeyValueParameterViewModel(this, p)));
      else
        Parameters = new ObservableCollection<KeyValueParameterViewModel>();
      MergeParametersFromTemplate();
    }


    private void MergeParametersFromTemplate()
    {
      try
      {
        UriTemplate template = new UriTemplate(Template);
        foreach (string name in template.QueryValueVariableNames)
        {
          bool existsInParameters = Parameters.Any(p => name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase));
          if (!existsInParameters)
          {
            KeyValueParameterViewModel p = new KeyValueParameterViewModel(this, name);
            Parameters.Add(p);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(GetOwnerWindow(), "Failed to read URL template: " + ex.Message);
      }
    }


    #region Commands

    private void OpenLinkTemplate(object arg)
    {
      Publish(new MasonViewModel.SourceChangedEventArgs { Source = JsonValue.ToString() });
      UrlTemplatePopupDialog d = new UrlTemplatePopupDialog(this);
      if (Parameters.Count > 0)
        Parameters[0].IsFocused = true;
      d.Owner = Window.GetWindow(arg as DependencyObject);
      d.ShowDialog();
    }


    private void Submit(object sender)
    {
      Dictionary<string, string> values = new Dictionary<string, string>();
      foreach (KeyValueParameterViewModel p in Parameters)
        values[p.Name] = p.Value;

      ISession session = RamoneServiceManager.Service.NewSession();

      Request req = session.Bind(Template, values).Method("GET");

      Window w = Window.GetWindow(sender as DependencyObject);

      Publish(new ExecuteWebRequestEventArgs(session, req) { OnSuccess = (r => HandleSuccess(r, w)) });
    }

    
    private void HandleSuccess(Response r, Window w)
    {
      w.Close();
    }


    private void Cancel(object sender)
    {
      Window w = Window.GetWindow(sender as DependencyObject);
      w.Close();
    }

    #endregion
  }
}
