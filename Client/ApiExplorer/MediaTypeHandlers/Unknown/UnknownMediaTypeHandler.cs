using ApiExplorer.Utilities;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Unknown
{
  public class UnknownMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(Response r)
    {
      return new UnknownMediaTypeRender();
    }

    #endregion
  }
}
