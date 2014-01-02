using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class PropertyViewModel : ViewModel
  {
    #region UI properties

    public string Name { get; set; }

    public object Value { get; set; }

    private bool _isExpanded;
    public bool IsExpanded
    {
      get { return _isExpanded; }
      set
      {
        if (value != _isExpanded)
        {
          _isExpanded = value;
          OnPropertyChanged("IsExpanded");
        }
      }
    }

    #endregion


    public PropertyViewModel(ViewModel parent)
      : base(parent)
    {
      IsExpanded = true;
    }
  }


  public class ResourcePropertyViewModel : PropertyViewModel
  {
    /*
    #region UI properties

    public string Name { get; set; }

    public ResourceViewModel Value { get; set; }

    #endregion
*/

    public ResourcePropertyViewModel(ViewModel parent)
      : base(parent)
    {
    }
  }
}
