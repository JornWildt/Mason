using Microsoft.Practices.Composite.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApiExplorer.ViewModels
{
  public class ComposerFileViewModel : ViewModel
  {
    #region UI Properties

    private string _name;
    public string Name
    {
      get { return _name; }
      set
      {
        if (value != _name)
        {
          _name = value;
          OnPropertyChanged("Name");
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


    private string _filename;
    public string Filename
    {
      get { return _filename; }
      set
      {
        if (value != _filename)
        {
          _filename = value;
          OnPropertyChanged("Filename");
        }
      }
    }
 
    #endregion


    #region Commands

    public DelegateCommand<FrameworkElement> SelectFileCommand { get; private set; }

    #endregion


    public ComposerFileViewModel(ViewModel parent)
      : base(parent)
    {
      RegisterCommand(SelectFileCommand = new DelegateCommand<FrameworkElement>(SelectFile));
    }


    private void SelectFile(FrameworkElement sender)
    {
      Window w = Window.GetWindow(sender);
      Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
      bool? result = dlg.ShowDialog(w);
      if (result == true)
      {
        Filename = dlg.FileName;
      }
      else
      {
        Filename = "";
      }
    }
  }
}
