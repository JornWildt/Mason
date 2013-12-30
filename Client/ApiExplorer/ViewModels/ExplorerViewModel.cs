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


    private string _responseStatus;
    public string ResponseStatus
    {
      get { return _responseStatus; }
      set
      {
        if (value != _responseStatus)
        {
          _responseStatus = value;
          OnPropertyChanged("ResponseStatus");
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
      //Url = "http://localhost/mason-demo/service-index";
      Url = "http://localhost/mason-demo/origin";
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
          .OnError(HandleResponseError)
          .Get(HandleResponse);
    }


    protected void HandleResponse(Response r)
    {
      Application.Current.Dispatcher.Invoke(() =>
        {
          IsExecutingRequest = false;
          ResponseStatus = string.Format("{0} {1}", (int)r.StatusCode, r.StatusCode.ToString());

          if (ContentRender is IDisposable)
            ((IDisposable)ContentRender).Dispose();

          IHandleMediaType handler = MediaTypeDispatcher.GetMediaTypeHandler(r);
          ContentRender = handler.GetRender(this, r);
          Url = r.BaseUri.AbsoluteUri;
        });
    }


    protected void HandleResponseError(AsyncError err)
    {
      Application.Current.Dispatcher.Invoke(() =>
        {
          IsExecutingRequest = false;
          ResponseStatus = string.Format("{0} {1}", (int)err.Response.StatusCode, err.Response.StatusCode.ToString());

          ContentRender = null;
          MessageBox.Show(err.Exception.Message);
        });
    }

    #endregion
  }
}
