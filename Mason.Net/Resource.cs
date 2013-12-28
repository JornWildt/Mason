using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;


namespace Mason.Net
{
  public class Resource : SubResource
  {
    [JsonProperty("mason:namespaces")]
    public List<Namespace> Namespaces { get; set; }

    [JsonProperty("mason:meta")]
    public DynamicDictionary Meta { get; set; }


    public void SetMeta(string key, object value)
    {
      if (Meta == null)
        Meta = new DynamicDictionary();
      Meta[key] = value;
    }


    public void AddNamespace(Namespace ns)
    {
      if (Namespaces == null)
        Namespaces = new List<Namespace>();
      Namespaces.Add(ns);
    }
  }
}
