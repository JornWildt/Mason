using ApiExplorer.MediaTypeHandlers.ApplicationMason.UserControls;
using ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class ApplicationMasonMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(ViewModel parent, Response r)
    {
      JObject resource = r.Decode<JObject>();

      MasonViewModel vm = new MasonViewModel(parent, resource);
      return new ApplicationMasonRender(vm);
    }

    #endregion
  }
}
