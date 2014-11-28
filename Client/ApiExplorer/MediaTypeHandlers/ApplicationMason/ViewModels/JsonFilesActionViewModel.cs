using ApiExplorer.ViewModels;
using Mason.Net;
using Newtonsoft.Json.Linq;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public class JsonFilesActionViewModel : JsonActionViewModel
  {
    public override string NavigationType { get { return MasonProperties.NavigationTypes.JSONFiles; } }


    public JsonFilesActionViewModel(ViewModel parent, JProperty json, BuilderContext context)
      : base(parent, json, context)
    {
    }


    protected override void ModifyComposerWindow(ComposerViewModel vm)
    {
      string title = (string.IsNullOrWhiteSpace(Title) ? "JSON+Files Action" : Title);
      vm.WindowTitle = title;
      vm.Description = Description;

      JToken jsonFile = OriginalJsonValue[MasonProperties.ActionProperties.JsonFile];
      if (jsonFile != null)
        vm.JsonFilename = jsonFile.Value<string>();

      JArray files = OriginalJsonValue[MasonProperties.ActionProperties.Files] as JArray;
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
