using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Mason.Net
{
  public class Link
  {
    [JsonIgnore]
    public string rel { get; set; }

    public string href { get; set; }

    public string title { get; set; }

    public string type { get; set; }

    public List<Link> alt { get; set; }


    public Link()
    {
    }


    public Link(string rel, Uri href, string title = null, string type = null)
      : this(rel, (href == null ? null : href.AbsoluteUri), title, type)
    {
    }


    public Link(string rel, string href, string title = null, string type = null)
    {
      this.rel = rel;
      this.href = href;
      this.title = title;
      this.type = type;
    }


    public void AddAltLink(Link l)
    {
      if (alt == null)
        alt = new List<Link>();
      alt.Add(l);
    }
  }
}
