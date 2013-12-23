using Newtonsoft.Json;
using System.Collections.Generic;


namespace Mason.Net
{
  public class SubResource : DynamicDictionary
  {
    [JsonProperty("mason:links")]
    public IList<Link> Links { get; private set; }


    public SubResource()
    {
      Links = new List<Link>();
    }
  }
}
