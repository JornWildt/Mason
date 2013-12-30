using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Unknown
{
  public class UnknownMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(ViewModel parent, Response r)
    {
      return new UnknownMediaTypeRender();
    }

    #endregion
  }
}
