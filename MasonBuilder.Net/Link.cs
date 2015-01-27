using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace MasonBuilder.Net
{
  public class Link : Control
  {
    public override string type { get { return null; } } // Default type is "link"


    public List<Link> alt { get; set; }


    public Link()
    {
    }


    public Link(string name, Uri href, string title = null)
      : this(name, (href == null ? null : href.AbsoluteUri), title)
    {
    }


    public Link(string name, string href, string title = null)
      : base(name, href, title)
    {
    }


    public void AddAlternateLink(Link l)
    {
      if (alt == null)
        alt = new List<Link>();
      alt.Add(l);
    }
  }
}
