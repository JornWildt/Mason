using ApiExplorer.Utilities;
using MasonBuilder.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ramone;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Resta.UriTemplates;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.ViewModels
{
  public class ComposerViewModel : ViewModel
  {
    public class MethodDefinition
    {
      public string Name { get; set; }
      public bool AllowContent { get; set; }
    }

    
    #region UI properties

    private string _windowTitle;
    public string WindowTitle
    {
      get { return _windowTitle; }
      set
      {
        if (value != _windowTitle)
        {
          _windowTitle = value;
          OnPropertyChanged("WindowTitle");
        }
      }
    }


    public ObservableCollection<MethodDefinition> Methods { get; set; }

    private string _method;
    public string Method
    {
      get { return _method; }
      set
      {
        if (value != _method)
        {
          _method = value;
          OnPropertyChanged(null);
        }
      }
    }


    public bool ContentRequired
    {
      get
      {
        MethodDefinition def = Methods.Where(m => m.Name == Method).FirstOrDefault();
        return IsHRefTemplate || def != null && def.AllowContent;
      }
    }


    private string _url;
    public string Url
    {
      get { return _url; }
      set
      {
        if (value != _url)
        {
          _url = value;
          OnPropertyChanged("Url");
        }
      }
    }


    private static string _headers;
    public string Headers
    {
      get { return _headers; }
      set
      {
        if (value != _headers)
        {
          _headers = value;
          OnPropertyChanged("Headers");
        }
      }
    }


    private string _title;
    public string Title
    {
      get { return _title; }
      set
      {
        if (value != _title)
        {
          _title = value;
          OnPropertyChanged("Title");
        }
      }
    }


    private string _description;
    public string Description
    {
      get { return _description; }
      set
      {
        if (value != _description)
        {
          _description = value;
          OnPropertyChanged("Description");
        }
      }
    }


    private string _body;
    public string Body
    {
      get { return _body; }
      set
      {
        if (value != _body)
        {
          _body = value;
          OnPropertyChanged("Body");
        }
      }
    }


    public ObservableCollection<string> Types { get; set; }


    private string _selectedType;
    public string SelectedType
    {
      get { return _selectedType; }
      set
      {
        if (value != _selectedType)
        {
          _selectedType = value;
          OnPropertyChanged(null);
        }
      }
    }


    public bool ShowTextEditor
    {
      get { return (SelectedType == MasonProperties.SerializationTypes.JSON || IsHRefTemplate) && ContentRequired; }
    }


    public bool ShowTextEditorWithFiles
    {
      get { return SelectedType == MasonProperties.SerializationTypes.JSONFiles && ContentRequired; }
    }


    public ObservableCollection<ComposerFileViewModel> Files { get; set; }


    #endregion


    #region Commands

    public DelegateCommand<FrameworkElement> ExecuteCommand { get; private set; }

    #endregion

    
    public string JsonPartName { get; set; }

    public bool IsHRefTemplate { get; set; }


    public ComposerViewModel(ViewModel parent)
      : base(parent)
    {
      RegisterCommand(ExecuteCommand = new DelegateCommand<FrameworkElement>(Execute));
      Url = "";
      Methods = new ObservableCollection<MethodDefinition>
      {
        new MethodDefinition { Name = "GET", AllowContent = false },
        new MethodDefinition { Name = "POST", AllowContent = true },
        new MethodDefinition { Name = "PUT", AllowContent = true },
        new MethodDefinition { Name = "DELETE", AllowContent = false },
        new MethodDefinition { Name = "PATCH", AllowContent = true }
      };
      Types = new ObservableCollection<string>();
      Types.Add(MasonProperties.SerializationTypes.None);
      Types.Add(MasonProperties.SerializationTypes.JSON);
      Types.Add(MasonProperties.SerializationTypes.JSONFiles);
      Types.Add(MasonProperties.SerializationTypes.Raw);
      SelectedType = MasonProperties.SerializationTypes.JSON;
      Files = new ObservableCollection<ComposerFileViewModel>();
    }


    private void Execute(FrameworkElement sender)
    {
      string url = Url;
      if (IsHRefTemplate)
      {
        try
        {
          JObject json = JsonConvert.DeserializeObject(Body) as JObject;
          if (json != null)
          {
            Resta.UriTemplates.UriTemplate t = new Resta.UriTemplates.UriTemplate(Url);
            IDictionary<string, object> parameters = new JsonObjectDictionary(json);
            url = t.Resolve(parameters);
          }
        }
        catch (Exception)
        {
          MessageBox.Show(GetOwnerWindow(), "Invalid URL template or template values");
          return;
        }
      }

      try
      {
        Uri tmp = new Uri(Url);
      }
      catch (Exception)
      {
        MessageBox.Show(GetOwnerWindow(), "Invalid URL");
        return;
      }

      if (string.IsNullOrEmpty(Method))
      {
        MessageBox.Show(GetOwnerWindow(), "Missing HTTP method");
        return;
      }

      ISession session = RamoneServiceManager.Session;

      Request req = session.Bind(url).Method(Method);

      if (Headers != null)
      {
        foreach (string line in Headers.Split('\n'))
        {
          int colonPos = line.IndexOf(':');
          if (colonPos > 0)
          {
            string header = line.Substring(0, colonPos).Trim();
            string value = line.Substring(colonPos + 1).Trim();
            if (!string.IsNullOrEmpty(header) && !string.IsNullOrEmpty(value))
            {
              if (header.Equals("accept", StringComparison.InvariantCultureIgnoreCase))
                req.Accept(value);
              else if (!header.Equals("content-type", StringComparison.InvariantCultureIgnoreCase))
                req.Header(header, value);
            }
          }
        }
      }

      if (SelectedType == MasonProperties.SerializationTypes.JSON && Body != null)
      {
        req.AsJson();
        req.Body(Body);
      }
      else if (SelectedType == MasonProperties.SerializationTypes.JSONFiles)
      {
        req.AsMultipartFormData();
        Hashtable files = new Hashtable();
        if (Body != null && !string.IsNullOrEmpty(JsonPartName))
          files[JsonPartName] = new Ramone.IO.StringFile { Filename = JsonPartName, ContentType = "application/json", Data = Body };
        foreach (ComposerFileViewModel file in Files)
        {
          if (!string.IsNullOrEmpty(file.Name) && !string.IsNullOrEmpty(file.Filename) && System.IO.File.Exists(file.Filename))
            files[file.Name] = new Ramone.IO.File(file.Filename);
        }
        req.Body(files);
      }

      Window w = Window.GetWindow(sender as DependencyObject);

      Publish(new ExecuteWebRequestEventArgs(session, req) { OnSuccess = (r => HandleSuccess(r, w)) });
    }


    private void HandleSuccess(Response r, Window w)
    {
      w.Close();
    }
  }
}
