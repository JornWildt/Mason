using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;


namespace Mason.Net
{
  public class Resource : DynamicDictionary
  {
    [JsonProperty("mason:namespaces")]
    public IList<Namespace> Namespaces { get; private set; }


    [JsonProperty("mason:links")]
    public IList<Link> Links { get; private set; }


    public Resource()
    {
      Namespaces = new List<Namespace>();
      Links = new List<Link>();
    }
  }
}
