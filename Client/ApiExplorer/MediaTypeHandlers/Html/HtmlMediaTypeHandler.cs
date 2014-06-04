using ApiExplorer.MediaTypeHandlers.Html.UserControls;
using ApiExplorer.MediaTypeHandlers.Html.ViewModels;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Ramone;
using System.IO;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Html
{
  public class HtmlMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(ViewModel parent, Response r)
    {
      string filename = Path.GetTempFileName();
      r.SaveToFile(filename);

      HtmlViewModel vm = new HtmlViewModel(parent, filename);
      return new HtmlRender(vm);
    }

    #endregion
  }
}
