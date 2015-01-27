using Newtonsoft.Json;
using System.Collections.Generic;


namespace MasonBuilder.Net
{
  public class SubResource : DynamicDictionary
  {
    [JsonProperty(MasonProperties.Prefix + "controls")]
    public Dictionary<string,Control> Controls { get; set; }


    public void AddControl(Control nav)
    {
      if (Controls == null)
        Controls = new Dictionary<string, Control>();
      Controls[nav.name] = nav;
    }
  }
}
