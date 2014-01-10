using ApiExplorer.ViewModels;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class MultipartJsonActionViewModel : ActionViewModel
  {
    public MultipartJsonActionViewModel(ViewModel parent, JToken json)
      : base(parent, json)
    {
    }
  }
}
