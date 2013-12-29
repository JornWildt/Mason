using ApiExplorer.Utilities;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class ApplicationMasonMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(Response r)
    {
      MasonViewModel vm = new MasonViewModel(null, "SOURCE ...");
      return new ApplicationMasonRender(vm);
    }

    #endregion
  }
}
