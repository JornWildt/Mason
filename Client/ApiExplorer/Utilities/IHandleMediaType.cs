using ApiExplorer.ViewModels;
using Ramone;
using System.Windows.Controls;


namespace ApiExplorer.Utilities
{
  public interface IHandleMediaType
  {
    UserControl GetRender(ViewModel parent, Response r);
  }
}
