using ApiExplorer.Utilities;
using ApiExplorer.Windows;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ramone;
using Ramone.HyperMedia;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Net;


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

    private bool _addressIsFocused;
    public bool AddressIsFocused
    {
      get { return _addressIsFocused; }
      set
      {
        _addressIsFocused = value;
        OnPropertyChanged("AddressIsFocused");
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

    
    #region Sub view models

    public NavigationViewModel Navigation { get; set; }

    #endregion

    
    #region Commands

    public DelegateCommand<object> GoCommand { get; private set; }
    public DelegateCommand<FrameworkElement> ComposeCommand { get; private set; }
    public DelegateCommand<object> AddressFocusCommand { get; private set; }

    #endregion


    public ExplorerViewModel(ViewModel parent)
      : base(parent)
    {
      Navigation = new NavigationViewModel(this);
      RegisterCommand(GoCommand = new DelegateCommand<object>(Go));
      RegisterCommand(ComposeCommand = new DelegateCommand<FrameworkElement>(Compose));
      RegisterCommand(AddressFocusCommand = new DelegateCommand<object>(AddressFocus));
      Subscribe<ExecuteWebRequestEventArgs>(e => ExecuteWebRequest(e));
      Subscribe<SetStatusLineTextEventArgs>(e => SetUpdateStatusLine(e.Text));
      Subscribe<ResetStatusLineTextEventArgs>(e => ResetUpdateStatusLine());
      Subscribe<NavigationViewModel.NavigateEventArgs>(e => Navigate());

      //Url = "http://localhost/mason-demo/projects";
      //Url = "http://localhost/mason-demo//issues/query";
      //Url = "http://localhost/mason-demo/resource-common";
      //Url = "http://localhost/mason-demo/contact";
      //Url = "http://localhost/mason-demo/projects/1";
      Navigation.CurrentUrl = "http://localhost/mason-demo/issues/1";
      //Url = "http://jorn-pc/mason-demo/projects/1/issues";
    }


    #region Address focus

    private void AddressFocus(object arg)
    {
      AddressIsFocused = true;
    }

    #endregion

    #region Navigation

    private void Navigate()
    {
      Go(null);
    }


    private void Go(object obj)
    {
      ISession session = RamoneServiceManager.Service.NewSession();

      Request req = session.Bind(Navigation.CurrentUrl).Method("GET");

      ExecuteWebRequest(new ExecuteWebRequestEventArgs { Request = req });
    }


    protected HttpWebRequest CurrentRequest { get; set; }

    protected void ExecuteWebRequest(ExecuteWebRequestEventArgs args)
    {
      IsExecutingRequest = true;

      if (CurrentRequest != null)
      {
        // CurrentRequest.Abort(); FIXME: needs Ramone to handle this
        CurrentRequest = null;
      }

      try
      {
        args.Request
            .Accept("application/vnd.mason;q=1, */*;q=0.5")
            .OnHeadersReady(r => { CurrentRequest = r; })
            .Async()
            .OnError(r => HandleResponseError(r, args))
            .OnComplete(() => CurrentRequest = null)
            .Submit(r => HandleResponse(r, args));
      }
      catch (Exception ex)
      {
        MessageBox.Show(GetOwnerWindow(), ex.Message, "Failed to setup request");
      }
    }


    protected void HandleResponse(Response r, ExecuteWebRequestEventArgs args)
    {
      Application.Current.Dispatcher.Invoke(() =>
        {
          IsExecutingRequest = false;
          StatusLine = string.Format("{0} {1}", (int)r.StatusCode, r.StatusCode.ToString());
          PreviousStatusLine = null;

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

          RenderResponse(r);
        });
    }


    protected void HandleResponseError(AsyncError err, ExecuteWebRequestEventArgs args)
    {
      Application.Current.Dispatcher.Invoke(() =>
        {
          IsExecutingRequest = false;
          StatusLine = string.Format("{0} {1}", (int)err.Response.StatusCode, err.Response.StatusCode.ToString());
          PreviousStatusLine = null;

          if (args.OnFailure != null)
            args.OnFailure(err.Response);

          RenderResponse(err.Response);
          MessageBox.Show(GetOwnerWindow(), err.Exception.Message);
        });
    }


    private void RenderResponse(Response r)
    {
      string contentType = r.ContentType.ToString();
      if (!string.IsNullOrEmpty(contentType))
        StatusLine += " [" + contentType + "]";

      IHandleMediaType handler = MediaTypeDispatcher.GetMediaTypeHandler(r);
      ContentRender = handler.GetRender(this, r);
      Navigation.CurrentUrl = r.WebResponse.ResponseUri.AbsoluteUri;
      if (r.WebResponse.Method == "GET")
        Navigation.RegisterUrl();
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
      if (PreviousStatusLine != null)
        StatusLine = PreviousStatusLine;
      PreviousStatusLine = null;
    }

    #endregion

    
    #region Compose

    private void Compose(FrameworkElement sender)
    {
      ComposerWindow.OpenComposerWindow(Window.GetWindow(sender), this, "GET", Navigation.CurrentUrl);
    }

    #endregion
  }
}
