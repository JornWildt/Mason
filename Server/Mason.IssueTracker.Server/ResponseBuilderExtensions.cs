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


    public static Link NewLink(this ICommunicationContext contex, string name, Uri href, string title = null, string contentType = null)
    {
      return contex.NewLink(name, href.AbsoluteUriNullable(), title);
    }


    public static Link NewLink(this ICommunicationContext contex, string name, string href, string title = null, string contentType = null)
    {
      if (contex.PreferMinimalResponse())
      {
        return new Link(name, href, null) { target_type = contentType };
      }
      else
      {
        return new Link(name, href, title) { target_type = contentType };
      }
    }


    public static LinkTemplate NewLinkTemplate(this ICommunicationContext context, string name, string template, string title = null, string description = null)
    {
      if (context.PreferMinimalResponse())
      {
        return new LinkTemplate(name, template, null) { description = description };
      }
      else
      {
        return new LinkTemplate(name, template, title) { description = description };
      }
    }


    public static Net.JsonAction NewJsonAction(this ICommunicationContext contex, string name, Uri href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = null)
    {
      if (contex.PreferMinimalResponse())
      {
        return new Net.JsonAction(name, href.AbsoluteUriNullable(), null, method)
        {
          description = description,
          schema = schema,
          schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null),
          template = template
        };
      }
      else
      {
        return new Net.JsonAction(name, href.AbsoluteUriNullable(), title, method)
        {
          description = description,
          schema = schema,
          schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null),
          template = template
        };
      }
    }


    public static Net.JsonFilesAction NewJsonFilesAction(this ICommunicationContext contex, string name, Uri href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = null)
    {
      if (contex.PreferMinimalResponse())
      {
        return new Net.JsonFilesAction(name, href.AbsoluteUriNullable(), null, method)
        {
          description = description,
          schema = schema,
          schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null),
          template = template
        };
      }
      else
      {
        return new Net.JsonFilesAction(name, href.AbsoluteUriNullable(), title, method)
        {
          description = description,
          schema = schema,
          schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null),
          template = template
        };
      }
    }


    public static string AbsoluteUriNullable(this Uri url)
    {
      return url == null ? null : url.AbsoluteUri;
    }
  }
}
