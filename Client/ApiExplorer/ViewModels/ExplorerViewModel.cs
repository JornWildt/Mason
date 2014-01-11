using ApiExplorer.Utilities;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ramone;
using Ramone.HyperMedia;
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


    private bool _addressIsFocused;
    public bool AddressIsFocused
    {
      get { return _addressIsFocused; }
      set
      {
        if (value != _addressIsFocused)
        {
          _addressIsFocused = value;
          OnPropertyChanged("AddressIsFocused");
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
    public DelegateCommand<object> AddressFocusCommand { get; private set; }

    #endregion


    public ExplorerViewModel(ViewModel parent)
      : base(parent)
    {
      RegisterCommand(GoCommand = new DelegateCommand<object>(Go));
      RegisterCommand(AddressFocusCommand = new DelegateCommand<object>(AddressFocus));
      Subscribe<ExecuteWebRequestEventArgs>(e => ExecuteWebRequest(e));
      Subscribe<SetStatusLineTextEventArgs>(e => SetUpdateStatusLine(e.Text));
      Subscribe<ResetStatusLineTextEventArgs>(e => ResetUpdateStatusLine());

      //Url = "http://localhost/mason-demo//issues/query";
      Url = "http://localhost/mason-demo/service-index";
      //Url = "http://localhost/mason-demo/resource-common";
    }


    #region Address focus

    private void AddressFocus(object arg)
    {
      AddressIsFocused = true;
    }

    #endregion


    #region Go command

    private void Go(object obj)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req = session.Bind(Url).Method("GET");

      ExecuteWebRequest(new ExecuteWebRequestEventArgs { Request = req });
    }


    protected void ExecuteWebRequest(ExecuteWebRequestEventArgs args)
    {
      IsExecutingRequest = true;

      args.Request
          .Accept("application/vnd.mason;q=1, */*;q=0.5")
          .Async()
          .OnError(r => HandleResponseError(r, args))
          .Submit(r => HandleResponse(r, args));
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

          // In case of "201 Created" with Location header: GET new location (but keep same success/failure handlers)
          if (r.CreatedLocation != null)
          {
            Request req = r.CreatedLocation.Follow(r.Session).Method("GET");
            ExecuteWebRequestEventArgs args2 = new ExecuteWebRequestEventArgs
            {
              Request = req,
              OnSuccess = args.OnSuccess,
              OnFailure = args.OnFailure
            };
            ExecuteWebRequest(args2);
            return;
          }

          IHandleMediaType handler = MediaTypeDispatcher.GetMediaTypeHandler(r);
          ContentRender = handler.GetRender(this, r);
          Url = r.WebResponse.ResponseUri.AbsoluteUri;
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
