using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.Utilities
{
  public interface IHandleMediaType
  {
    UserControl GetRender(Response r);
  }
}
