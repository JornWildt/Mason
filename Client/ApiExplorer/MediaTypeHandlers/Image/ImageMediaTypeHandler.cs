using System.IO;
using System.Windows.Controls;
using ApiExplorer.MediaTypeHandlers.Image.UserControls;
using ApiExplorer.MediaTypeHandlers.Image.ViewModels;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Ramone;


namespace ApiExplorer.MediaTypeHandlers.Image
{
  public class ImageMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(ViewModel parent, Response r)
    {
      string filename = Path.GetTempFileName();
      r.SaveToFile(filename);

      ImageViewModel vm = new ImageViewModel(parent, filename);
      return new ImageRender(vm);
    }

    #endregion
  }
}
