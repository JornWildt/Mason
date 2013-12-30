using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Mason.Net;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ramone;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class PropertyViewModel : ViewModel
  {
    #region UI properties

    public string Name { get; set; }

    public string Value { get; set; }

    #endregion


    public PropertyViewModel(ViewModel parent)
      : base(parent)
    {
    }
  }
}
