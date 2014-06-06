using Microsoft.Practices.Composite.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ApiExplorer.ViewModels
{
  public class BasicAuthorizationSetupViewModel : ViewModel
  {
    private string _prompt;
    public string Prompt
    {
      get { return _prompt; }
      set
      {
        if (value != _prompt)
        {
          _prompt = value;
          OnPropertyChanged("Prompt");
        }
      }
    }


    private string _username;
    public string Username
    {
      get { return _username; }
      set
      {
        if (value != _username)
        {
          _username = value;
          OnPropertyChanged("Username");
        }
      }
    }


    private string _password;
    public string Password
    {
      get { return _password; }
      set
      {
        if (value != _password)
        {
          _password = value;
          OnPropertyChanged("Password");
        }
      }
    }


    public bool Success { get; set; }

    
    #region Commands

    public DelegateCommand<object> OkCommand { get; private set; }
    public DelegateCommand<object> CancelCommand { get; private set; }

    #endregion


    public BasicAuthorizationSetupViewModel(ViewModel parent, string prompt)
      : base(parent)
    {
      Prompt = prompt;
      RegisterCommand(OkCommand = new DelegateCommand<object>(Ok));
      RegisterCommand(CancelCommand = new DelegateCommand<object>(Cancel));
    }


    public void Ok(object args)
    {
      Success = true;
      CloseWindow();
    }


    public void Cancel(object args)
    {
      CloseWindow();
    }
  }
}
