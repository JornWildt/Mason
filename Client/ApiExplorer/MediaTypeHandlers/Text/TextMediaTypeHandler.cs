using ApiExplorer.MediaTypeHandlers.Text.UserControls;
using ApiExplorer.MediaTypeHandlers.Text.ViewModels;
using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.MediaTypeHandlers.Text
{
  public class TextMediaTypeHandler : IHandleMediaType
  {
    #region IHandleMediaType Members

    public UserControl GetRender(ViewModel parent, Response r)
    {
      string text = r.Decode<string>();

      TextViewModel vm = new TextViewModel(parent, text);
      return new TextRender(vm);
    }

    #endregion
  }
}
