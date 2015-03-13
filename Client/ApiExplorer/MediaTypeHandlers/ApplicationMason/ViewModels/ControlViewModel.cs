using ApiExplorer.Utilities;
using ApiExplorer.ViewModels;
using Microsoft.Practices.Composite.Presentation.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Ramone;
using System;
using System.Collections.ObjectModel;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason.ViewModels
{
  public abstract class ControlViewModel : ElementViewModel
  {
    public abstract string ControlType { get; }

    public string Name { get; set; }

    public string Encoding { get { return GetValue<string>("encoding", "none"); } }

    public string Title { get { return GetValue<string>("title"); } }

    public string Description { get { return GetValue<string>("description"); } }

    public string HRef { get { return GetValue<string>("href"); } }

    public bool IsHRefTemplate { get { return GetValue<bool>("isHrefTemplate", false); } }

    public string Method { get { return GetValue<string>("method", "GET").ToUpper(); } }

    public string NamePart1 { get; set; }

    public string NamePart2 { get; set; }


    public ObservableCollection<ControlViewModel> AlternateControls { get; set; }


    #region Commands

    public DelegateCommand<object> ActivateControlCommand { get; private set; }

    #endregion



    public ControlViewModel(ViewModel parent, string name, JObject json, BuilderContext context, IControlBuilder cb)
      : base(parent, json)
    {
      if (json == null)
        throw new InvalidOperationException(string.Format("Expected JSON object for control {0}", name));
      
      string prefix;
      string reference;
      string nsname;

      Name = context.Namespaces.Expand(name, out prefix, out reference, out nsname);

      RegisterCommand(ActivateControlCommand = new DelegateCommand<object>(ActivateControl));

      if (reference != null && nsname != null)
      {
        NamePart1 = nsname;
        NamePart2 = reference;
      }
      else
      {
        NamePart1 = "";
        NamePart2 = Name;
      }

      JArray alt = json["alt"] as JArray;
      if (alt != null)
      {
        AlternateControls = new ObservableCollection<ControlViewModel>();
        for (int i = 0; i < alt.Count; ++i)
        {
          JObject l = alt[i] as JObject;
          if (l != null)
            AlternateControls.Add(cb.BuildControlElement(this, name, l, context));
              //LinkViewModel(this, l, string.Format("alt[{0}]", i), context));
        }
      }
    }


    protected abstract void ActivateControl(object args);


    protected string CalculateJsonPayload()
    {
      string jsonText = null;

      JToken templateJson = OriginalJsonValue["template"];

      JToken schemaJson = OriginalJsonValue["schema"];
      JObject schema = schemaJson as JObject;

      JToken schemaUrlJson = OriginalJsonValue["schemaUrl"];
      string schemaUrl = (schemaUrlJson != null ? schemaUrlJson.Value<string>() : null);

      if (templateJson != null)
      {
        jsonText = templateJson.ToString();
      }
      else if (schema != null)
      {
        JsonSchema jschema = JsonSchema.Parse(schemaJson.ToString());
        JsonExampleGenerator generator = new JsonExampleGenerator();
        jsonText = generator.GenerateJsonInstanceFromSchema(jschema);
      }
      else if (!string.IsNullOrEmpty(schemaUrl))
      {
        try
        {
          using (var response = RamoneServiceManager.Session.Bind(schemaUrl).Get<string>())
          {
            JsonSchema jschema = JsonSchema.Parse(response.Body);
            JsonExampleGenerator generator = new JsonExampleGenerator();
            jsonText = generator.GenerateJsonInstanceFromSchema(jschema);
          }
        }
        catch (Exception ex)
        {
          jsonText = string.Format("Failed to retrieve JSON schema from '{0}': {1}", schemaUrl, ex.Message);
        }
      }

      return jsonText;
    }
  }
}
