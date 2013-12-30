using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;


namespace ApiExplorer.ViewModels
{
  public class ViewModel : INotifyPropertyChanged
  {
    public ViewModel(ViewModel parent)
    {
      Parent = parent;
      if (Parent != null)
        Parent.RegisterChildViewModel(this);
      Events = new EventAggregator();
    }


    #region Parent/child managing

    private ViewModel _parent;
    public ViewModel Parent
    {
      get { return _parent; }
      set
      {
        if (value != _parent)
        {
          _parent = value;
          OnPropertyChanged("Parent");
        }
      }
    }


    private IList<ViewModel> ChildViewModels = new List<ViewModel>();


    protected void RegisterChildViewModel(ViewModel viewModel)
    {
      ChildViewModels.Add(viewModel);
    }


    protected void ClearChildViewModels()
    {
      ChildViewModels.Clear();
    }

    #endregion


    #region Command handling

    protected List<ICommand> Commands = new List<ICommand>();

    public void RegisterCommand<T>(DelegateCommand<T> command)
    {
      Commands.Add(command);
    }

    #endregion


    #region Event bubling

    protected EventAggregator Events { get; private set; }


    protected void Publish<TPayload>(TPayload p)
    {
      Events.GetEvent<CompositePresentationEvent<TPayload>>().Publish(p);
      if (Parent != null)
        Parent.Publish(p);
    }

    
    protected void Subscribe<TPayload>(Action<TPayload> action)
    {
      Events.GetEvent<CompositePresentationEvent<TPayload>>().Subscribe(action);
    }

    #endregion


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;


    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
        handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}
