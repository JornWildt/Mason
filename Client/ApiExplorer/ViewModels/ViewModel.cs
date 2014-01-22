using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;


namespace ApiExplorer.ViewModels
{
  public class ViewModel : INotifyPropertyChanged
  {
    public ViewModel(ViewModel parent, Window owner = null)
    {
      Parent = parent;
      OwnerWindow = owner;
      if (Parent != null)
        Parent.AddChildViewModel(this);
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


    protected void AddChildViewModel(ViewModel vm)
    {
      ChildViewModels.Add(vm);
    }


    protected void RemoveChildViewModel(ViewModel vm)
    {
      ChildViewModels.Remove(vm);
    }


    protected void ClearChildViewModels()
    {
      ChildViewModels.Clear();
    }


    public void RemoveFromParent()
    {
      if (Parent != null)
        Parent.RemoveChildViewModel(this);
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


    public void Publish<TPayload>(TPayload p)
    {
      Events.GetEvent<CompositePresentationEvent<TPayload>>().Publish(p);
      if (Parent != null)
        Parent.Publish(p);
    }

    
    public void Subscribe<TPayload>(Action<TPayload> action)
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


    #region Window handling

    private Window OwnerWindow { get; set; }


    protected Window GetOwnerWindow()
    {
      if (OwnerWindow != null)
        return OwnerWindow;
      if (Parent != null)
        return Parent.GetOwnerWindow();

      throw new InvalidOperationException("No Window supplied in any parent ViewModel");
    }

    #endregion
  }
}
