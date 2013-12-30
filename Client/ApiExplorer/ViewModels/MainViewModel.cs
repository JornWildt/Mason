using ApiExplorer.Utilities;
using System.Windows;


namespace ApiExplorer.ViewModels
{
  public class TitleChangedEventArgs
  {
    public string Title { get; set; }
  }


  public class MainViewModel : ViewModel
  {
    #region UI properties

    private string _title;
    public string Title
    {
      get { return _title; }
      set
      {
        if (value != _title)
        {
          _title = value;
          OnPropertyChanged("Title");
        }
      }
    }


    public ExplorerViewModel Explorer { get; set; }

    #endregion


    #region Commands

    #endregion


    public MainViewModel()
      : base(null)
    {
      Explorer = new ExplorerViewModel(this);
      Title = "API Explorer";
      Subscribe<TitleChangedEventArgs>(e => Title = e.Title);
    }
  }
}
