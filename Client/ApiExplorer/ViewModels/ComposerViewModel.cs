using ApiExplorer.Utilities;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ramone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApiExplorer.ViewModels
{
  public class ComposerViewModel : ViewModel
  {
    #region UI properties

    private string _method;
    public string Method
    {
      get { return _method; }
      set
      {
        if (value != _method)
        {
          _method = value;
          OnPropertyChanged("Method");
        }
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


    #endregion


    #region Commands

    public DelegateCommand<FrameworkElement> ExecuteCommand { get; private set; }

    #endregion


    public ComposerViewModel(ViewModel parent)
      : base(parent)
    {
      RegisterCommand(ExecuteCommand = new DelegateCommand<FrameworkElement>(Execute));
      Url = "";
    }


    private void Execute(FrameworkElement sender)
    {
      try
      {
        Uri url = new Uri(Url);
      }
      catch (Exception)
      {
        MessageBox.Show("Invalid URL");
        return;
      }

      if (string.IsNullOrEmpty(Method))
      {
        MessageBox.Show("Missing HTTP method");
        return;
      }

      ISession session = RamoneServiceManager.Service.NewSession();

      Request req = session.Bind(Url).Method(Method);

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
              else if (header.Equals("content-type", StringComparison.InvariantCultureIgnoreCase))
                req.ContentType(value);
              else
                req.Header(header, value);
            }
          }
        }
      }

      if (Body != null)
        req.Body(Body);

      Window w = Window.GetWindow(sender as DependencyObject);

      Publish(new ExecuteWebRequestEventArgs { Request = req, OnSuccess = (r => HandleSuccess(r, w)) });
    }


    private void HandleSuccess(Response r, Window w)
    {
      w.Close();
    }
  }
}
