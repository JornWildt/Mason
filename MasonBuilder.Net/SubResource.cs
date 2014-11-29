using Newtonsoft.Json;
using System.Collections.Generic;


namespace MasonBuilder.Net
{
  public class SubResource : DynamicDictionary
  {
    [JsonProperty(MasonProperties.Prefix + "navigation")]
    public Dictionary<string,Navigation> Navigation { get; set; }


    public void AddNavigation(Navigation nav)
    {
      if (Navigation == null)
        Navigation = new Dictionary<string, Navigation>();
      Navigation[nav.name] = nav;
    }
  }
}
