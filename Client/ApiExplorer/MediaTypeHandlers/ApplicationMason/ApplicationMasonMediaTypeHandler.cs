using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Mason.Net;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class ApplicationMasonMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(ViewModel parent, Response r)
    {
      Resource resource = r.Decode<Resource>();

      MasonViewModel vm = new MasonViewModel(parent, resource);
      return new ApplicationMasonRender(vm);
    }

    #endregion
  }
}
