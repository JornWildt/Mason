using ApiExplorer.Utilities;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ramone;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace ApiExplorer.ViewModels
{
  public class ExecuteWebRequestEventArgs
  {
    public Request Request { get; set; }
    public Action<Response> OnSuccess { get; set; }
    public Action<Response> OnFailure { get; set; }
  }


  public class SetStatusLineTextEventArgs
  {
    public string Text { get; set; }
  }


  public class ResetStatusLineTextEventArgs
  {
  }


  public class ExplorerViewModel : ViewModel
  {
    #region UI properties

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


    private bool _isExecutingRequest;
    public bool IsExecutingRequest
    {
      get { return _isExecutingRequest; }
      set
      {
        if (value != _isExecutingRequest)
        {
          _isExecutingRequest = value;
          OnPropertyChanged("IsExecutingRequest");
        }
      }
    }


    private string _statusLine;
    public string StatusLine
    {
      get { return _statusLine; }
      set
      {
        if (value != _statusLine)
        {
          _statusLine = value;
          OnPropertyChanged("StatusLine");
        }
      }
    }


    private UserControl _contentRender;
    public UserControl ContentRender
    {
      get { return _contentRender; }
      set
      {
        if (value != _contentRender)
        {
          _contentRender = value;
          OnPropertyChanged("ContentRender");
        }
      }
    }

    #endregion


    #region Commands

    public DelegateCommand<object> GoCommand { get; private set; }

    #endregion


    public ExplorerViewModel(ViewModel parent)
      : base(parent)
    {
      RegisterCommand(GoCommand = new DelegateCommand<object>(Go));
      Subscribe<ExecuteWebRequestEventArgs>(e => ExecuteWebRequest(e));
      Subscribe<SetStatusLineTextEventArgs>(e => SetUpdateStatusLine(e.Text));
      Subscribe<ResetStatusLineTextEventArgs>(e => ResetUpdateStatusLine());
      
      Url = "http://localhost/mason-demo/service-index";
      //Url = "http://localhost/mason-demo/origin";
    }


    #region Go command

    private void Go(object obj)
    {
      ISession session = RamoneServiceManager.Service.NewSession();
      
      Request req = 
        session.Bind(Url)
               .Accept("application/vnd.mason;q=1, */*;q=0.5");

      ExecuteWebRequest(new ExecuteWebRequestEventArgs { Request = req });
    }


    protected void ExecuteWebRequest(ExecuteWebRequestEventArgs args)
    {
      IsExecutingRequest = true;

      args.Request
          .Async()
          .OnError(r => HandleResponseError(r, args))
          .Get(r => HandleResponse(r, args));
    }


    protected void HandleResponse(Response r, ExecuteWebRequestEventArgs args)
    {
      Application.Current.Dispatcher.Invoke(() =>
        {
          IsExecutingRequest = false;
          StatusLine = string.Format("{0} {1}", (int)r.StatusCode, r.StatusCode.ToString());

          if (args.OnSuccess != null)
            args.OnSuccess(r);

          if (ContentRender is IDisposable)
            ((IDisposable)ContentRender).Dispose();

          IHandleMediaType handler = MediaTypeDispatcher.GetMediaTypeHandler(r);
          ContentRender = handler.GetRender(this, r);
          Url = r.BaseUri.AbsoluteUri;
        });
    }


    protected void HandleResponseError(AsyncError err, ExecuteWebRequestEventArgs args)
    {
      Application.Current.Dispatcher.Invoke(() =>
        {
          IsExecutingRequest = false;
          StatusLine = string.Format("{0} {1}", (int)err.Response.StatusCode, err.Response.StatusCode.ToString());

          if (args.OnFailure != null)
            args.OnFailure(err.Response);

          ContentRender = null;
          MessageBox.Show(err.Exception.Message);
        });
    }

    #endregion


    #region Status line

    private string PreviousStatusLine;

    private void SetUpdateStatusLine(string text)
    {
      PreviousStatusLine = StatusLine;
      StatusLine = text;
    }


    private void ResetUpdateStatusLine()
    {
      StatusLine = PreviousStatusLine;
    }

    #endregion
  }
}
