using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class MultipartJsonActionViewModel : JsonActionViewModel
  {
    protected override string ActionType { get { return MasonProperties.ActionTypes.JSONFiles; } }


    public MultipartJsonActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json, context)
    {
    }


    protected override void ModifyComposerWindow(ComposerViewModel vm)
    {
      JToken jsonFile = JsonValue[MasonProperties.ActionProperties.JsonFile];
      if (jsonFile != null)
        vm.JsonFilename = jsonFile.Value<string>();

      JArray files = JsonValue[MasonProperties.ActionProperties.Files] as JArray;
      if (files != null)
      {
        foreach (JObject file in files.OfType<JObject>())
        {
          ComposerFileViewModel fileVm = new ComposerFileViewModel(vm);
          fileVm.Name = GetValue<string>(file, MasonProperties.ActionProperties.Files_Name);
          fileVm.Description = GetValue<string>(file, MasonProperties.ActionProperties.Files_Description);
          vm.Files.Add(fileVm);
        }
      }
    }
  }
}
