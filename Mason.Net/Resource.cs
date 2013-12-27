using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;


namespace Mason.Net
{
  public class Resource : DynamicDictionary
  {
    [JsonProperty("mason:namespaces")]
    public IList<Namespace> Namespaces { get; private set; }

    [JsonProperty("mason:meta")]
    public DynamicDictionary Meta { get; set; }

    [JsonProperty("mason:links")]
    public List<Link> Links { get; private set; }

    [JsonProperty("mason:link-templates")]
    public List<LinkTemplate> LinkTemplates { get; private set; }

    public Resource()
    {
      Namespaces = new List<Namespace>();
      Links = new List<Link>();
      LinkTemplates = new List<LinkTemplate>();
      Meta = new DynamicDictionary();
    }
  }
}
