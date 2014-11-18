using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;


namespace Mason.Net
{
  public class Resource : SubResource
  {
    #region Namespaces

    [JsonProperty(MasonProperties.Prefix + "namespaces")]
    public Dictionary<string, Namespace> Namespaces { get; set; }

    public void AddNamespace(Namespace ns)
    {
      if (Namespaces == null)
        Namespaces = new Dictionary<string, Namespace>();
      Namespaces[ns.prefix] = ns;
    }

    #endregion


    #region Meta

    private MetaElement _meta;

    [JsonIgnore]
    public MetaElement Meta
    {
      get
      {
        if (_meta == null)
          _meta = new MetaElement();
        return _meta;
      }
    }

    [JsonProperty(MasonProperties.Prefix + "meta")]
    public MetaElement SerializerMeta
    {
      get { return _meta; }
    }

    #endregion

    
    #region Error

    [JsonProperty(MasonProperties.Prefix + "error")]
    public ErrorElement Error { get; set; }

    #endregion
  }
}
