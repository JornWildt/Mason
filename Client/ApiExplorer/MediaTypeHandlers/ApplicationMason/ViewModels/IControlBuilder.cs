using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public interface IControlBuilder
  {
    ControlViewModel BuildControlElement(ViewModel parent, string name, JObject value, BuilderContext context);
  }
}
