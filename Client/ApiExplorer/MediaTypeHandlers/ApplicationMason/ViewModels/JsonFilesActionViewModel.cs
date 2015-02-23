using ApiExplorer.ViewModels;
using MasonBuilder.Net;
using Newtonsoft.Json.Linq;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class JsonFilesActionViewModel : JsonActionViewModel
  {
    public override string ControlType { get { return "JSON+Files"; } }


    public JsonFilesActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json, context)
    {
    }


    protected override void ModifyComposerWindow(ComposerViewModel vm)
    {
      JToken jsonFile = OriginalJsonValue[MasonProperties.ControlProperties.JsonFile];
      if (jsonFile != null)
        vm.JsonPartName = jsonFile.Value<string>();

      JArray files = OriginalJsonValue[MasonProperties.ControlProperties.Files] as JArray;
      if (files != null)
      {
        foreach (JObject file in files.OfType<JObject>())
        {
          ComposerFileViewModel fileVm = new ComposerFileViewModel(vm);
          fileVm.Name = GetValue<string>(file, MasonProperties.ControlPartProperties.Name);
          fileVm.Title = GetValue<string>(file, MasonProperties.ControlPartProperties.Title);
          fileVm.Description = GetValue<string>(file, MasonProperties.ControlPartProperties.Description);
          vm.Files.Add(fileVm);
        }
      }
    }
  }
}
