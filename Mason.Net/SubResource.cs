using Newtonsoft.Json;
using System.Collections.Generic;


namespace Mason.Net
{
  public class SubResource : DynamicDictionary
  {
    [JsonProperty(MasonProperties.Prefix + "links")]
    public Dictionary<string,Link> Links { get; set; }

    [JsonProperty(MasonProperties.Prefix + "link-templates")]
    public Dictionary<string, LinkTemplate> LinkTemplates { get; set; }

    [JsonProperty(MasonProperties.Prefix + "actions")]
    public Dictionary<string, Action> Actions { get; set; }


    public void AddLink(Link l)
    {
      if (Links == null)
        Links = new Dictionary<string, Link>();
      Links[l.rel] = l;
    }


    public void AddLinkTemplate(LinkTemplate t)
    {
      if (LinkTemplates == null)
        LinkTemplates = new Dictionary<string, LinkTemplate>();
      LinkTemplates[t.name] = t;
    }


    public void AddAction(Action a)
    {
      if (Actions == null)
        Actions = new Dictionary<string, Action>();
      Actions[a.name] = a;
    }
  }
}
