using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;


namespace Mason.Net
{
  public class Resource : SubResource
  {
    [JsonProperty(MasonProperties.Prefix + "namespaces")]
    public Dictionary<string, Namespace> Namespaces { get; set; }

    [JsonProperty(MasonProperties.Prefix + "meta")]
    public SubResource Meta { get; set; }


    public void SetMeta(string key, object value)
    {
      if (Meta == null)
        Meta = new SubResource();
      Meta[key] = value;
    }


    public void AddMetaLink(Link l)
    {
      if (Meta == null)
        Meta = new SubResource();
      Meta.AddLink(l);
    }


    public void AddNamespace(Namespace ns)
    {
      if (Namespaces == null)
        Namespaces = new Dictionary<string, Namespace>();
      Namespaces[ns.prefix] = ns;
    }
  }
}
