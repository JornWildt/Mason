using System;
using System.Windows.Input;


namespace ApiExplorer.Utilities
{
  public class DelegateCommand<T> : ICommand
  {
    private readonly Predicate<T> _canExecute;
    private readonly Action<T> _execute;

    
    public event EventHandler CanExecuteChanged;


    #region ICommand Members

    public bool CanExecute(object parameter)
    {
      return CanExecute((T)parameter);
    }


    public void Execute(object parameter)
    {
      Execute((T)parameter);
    }

    #endregion

    
    public DelegateCommand(Action<T> execute)
      : this(execute, null)
    {
    }


    public DelegateCommand(Action<T> execute,
                   Predicate<T> canExecute)
    {
      _execute = execute;
      _canExecute = canExecute;
    }


    public bool CanExecute(T parameter)
    {
      if (_canExecute == null)
      {
        return true;
      }

      return _canExecute(parameter);
    }

    
    public void Execute(T parameter)
    {
      _execute(parameter);
    }

    
    public void RaiseCanExecuteChanged()
    {
      if (CanExecuteChanged != null)
      {
        CanExecuteChanged(this, EventArgs.Empty);
      }
    }
  }
}
