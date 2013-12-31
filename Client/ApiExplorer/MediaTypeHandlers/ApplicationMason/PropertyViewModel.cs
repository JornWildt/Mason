using ApiExplorer.ViewModels;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class PropertyViewModel : ViewModel
  {
    #region UI properties

    public string Name { get; set; }

    public object Value { get; set; }

    #endregion


    public PropertyViewModel(ViewModel parent)
      : base(parent)
    {
    }
  }


  public class ResourcePropertyViewModel : ViewModel
  {
    #region UI properties

    public string Name { get; set; }

    public ResourceViewModel Value { get; set; }

    #endregion


    public ResourcePropertyViewModel(ViewModel parent)
      : base(parent)
    {
    }
  }
}
