using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace ApiExplorer.ViewModels
{
  public class NavigationViewModel : ViewModel
  {
    public class NavigateEventArgs
    {
    }


    #region UI Properties

    private string _currentUrl;
    public string CurrentUrl
    {
      get { return _currentUrl; }
      set
      {
        if (value != _currentUrl)
        {
          _currentUrl = value;
          OnPropertyChanged("CurrentUrl");
        }
      }
    }

    #endregion


    #region Commands

    public DelegateCommand<object> BackCommand { get; set; }

    #endregion


    #region Private properties

    private Stack<string> PreviousUrls { get; set; }

    #endregion


    public NavigationViewModel(ViewModel parent)
      : base(parent)
    {
      PreviousUrls = new Stack<string>();
      RegisterCommand(BackCommand = new DelegateCommand<object>(Back));

      CurrentUrl = Properties.Settings.Default.LastAccessedUrl;
      if (string.IsNullOrEmpty(CurrentUrl))
        CurrentUrl = "http://mason-issue-tracker.cbrain.net/issues/1";
    }


    public void RegisterUrl()
    {
      if (PreviousUrls.Count == 0 || PreviousUrls.Peek() != CurrentUrl)
      {
        PreviousUrls.Push(CurrentUrl);
        Properties.Settings.Default.LastAccessedUrl = CurrentUrl;
      }
    }


    private void Back(object args)
    {
      if (PreviousUrls.Count > 1)
      {
        PreviousUrls.Pop();
        CurrentUrl = PreviousUrls.Peek();
        Publish(new NavigateEventArgs());
      }
    }
  }
}
