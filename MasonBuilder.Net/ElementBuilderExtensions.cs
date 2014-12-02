using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasonBuilder.Net
{
  public static class ElementBuilderExtensions
  {
    //public static bool PreferMinimalResponse(this IMasonBuilderContext contex)
    //{
    //  return contex.Request.Headers.ContainsKey("Prefer") && contex.Request.Headers["Prefer"].Contains("return=minimal");
    //}


    public static Link NewLink(this IMasonBuilderContext contex, string name, Uri href, string title = null, string contentType = null)
    {
      return contex.NewLink(name, href.AbsoluteUriNullable(), title);
    }


    public static Link NewLink(this IMasonBuilderContext contex, string name, string href, string title = null, string contentType = null)
    {
      if (contex.PreferMinimalResponse)
      {
        return new Link(name, href, null) { target_type = contentType };
      }
      else
      {
        return new Link(name, href, title) { target_type = contentType };
      }
    }


    public static LinkTemplate NewLinkTemplate(this IMasonBuilderContext context, string name, string template, string title = null, string description = null)
    {
      if (context.PreferMinimalResponse)
      {
        return new LinkTemplate(name, template, null);
      }
      else
      {
        return new LinkTemplate(name, template, title) { description = description };
      }
    }


    public static VoidAction NewVoidAction(this IMasonBuilderContext contex, string name, Uri href, string title = null, string description = null, string method = null)
    {
      if (contex.PreferMinimalResponse)
      {
        return new VoidAction(name, href.AbsoluteUriNullable(), null, method);
      }
      else
      {
        return new VoidAction(name, href.AbsoluteUriNullable(), title, method)
        {
          description = description
        };
      }
    }


    public static JsonAction NewJsonAction(this IMasonBuilderContext contex, string name, Uri href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = null)
    {
      if (contex.PreferMinimalResponse)
      {
        return new JsonAction(name, href.AbsoluteUriNullable(), null, method)
        {
          template = template
        };
      }
      else
      {
        return new JsonAction(name, href.AbsoluteUriNullable(), title, method)
        {
          description = description,
          schema = schema,
          schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null),
          template = template
        };
      }
    }


    public static JsonFilesAction NewJsonFilesAction(this IMasonBuilderContext contex, string name, Uri href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = null)
    {
      if (contex.PreferMinimalResponse)
      {
        return new JsonFilesAction(name, href.AbsoluteUriNullable(), null, method)
        {
          template = template
        };
      }
      else
      {
        return new JsonFilesAction(name, href.AbsoluteUriNullable(), title, method)
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
