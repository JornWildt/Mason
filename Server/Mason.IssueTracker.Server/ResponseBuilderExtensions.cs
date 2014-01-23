using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server
{
  public static class ResponseBuilderExtensions
  {
    public static bool PreferMinimalResponse(this ICommunicationContext contex)
    {
      return contex.Request.Headers.ContainsKey("Prefer") && contex.Request.Headers["Prefer"].Contains("return=minimal");
    }


    public static Link NewLink(this ICommunicationContext contex, string rel, Uri href, string title = null, string type = null)
    {
      return contex.NewLink(rel, (href == null ? null : href.AbsoluteUri), title, type);
    }


    public static Link NewLink(this ICommunicationContext contex, string rel, string href, string title = null, string type = null)
    {
      if (contex.PreferMinimalResponse())
      {
        return new Link(rel, href, null, type);
      }
      else
      {
        return new Link(rel, href, title, type);
      }
    }


    public static LinkTemplate NewLinkTemplate(this ICommunicationContext context, string name, string template, string description = null)
    {
      if (context.PreferMinimalResponse())
      {
        return new LinkTemplate(name, template, null);
      }
      else
      {
        return new LinkTemplate(name, template, description);
      }
    }


    public static Net.Action NewAction(this ICommunicationContext contex, string name, string type, Uri href, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = null)
    {
      if (contex.PreferMinimalResponse())
      {
        return new Net.Action(name, type, href, null, null, null, null, method);
      }
      else
      {
        return new Net.Action(name, type, href, description, schema, schemaUrl, template, method);
      }
    }
  }
}
