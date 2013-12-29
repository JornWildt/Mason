using ApiExplorer.Utilities;
using System.Windows;


namespace ApiExplorer.ViewModels
{
  public class MainViewModel : ViewModel
  {
    #region UI properties

    public ExplorerViewModel Explorer { get; set; }

    #endregion


    #region Commands

    #endregion


    public MainViewModel()
      : base(null)
    {
      Explorer = new ExplorerViewModel(this);
    }
  }
}
