using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class MultipartJsonActionViewModel : JsonActionViewModel
  {
    protected override string ActionType { get { return MasonProperties.ActionTypes.JSONFiles; } }


    public MultipartJsonActionViewModel(ViewModel parent, JProperty json)
      : base(parent, json)
    {
    }
  }
}
