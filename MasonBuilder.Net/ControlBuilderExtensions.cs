using System;


namespace MasonBuilder.Net
{
  public static class ControlBuilderExtensions
  {
    public static Control NewLink(this IMasonBuilderContext context, string name, Uri href, string title = null, string contentType = null)
    {
      return context.NewLink(name, href.AbsoluteUriNullable(), title);
    }


    public static Control NewLink(this IMasonBuilderContext context, string name, string href, string title = null, string contentType = null)
    {
      if (context.PreferMinimalResponse)
      {
        return new Control(name, href, null) { output = (contentType != null ? new string[] { contentType } : null) };
      }
      else
      {
        return new Control(name, href, title) { output = (contentType != null ? new string[] { contentType } : null) };
      }
    }


    public static Control NewLinkTemplate(this IMasonBuilderContext context, string name, string template, string title = null, string description = null)
    {
      if (context.PreferMinimalResponse)
      {
        return new Control(name, template, null, true);
      }
      else
      {
        return new Control(name, template, title, true) { description = description };
      }
    }


    public static Control NewVoidAction(this IMasonBuilderContext context, string name, Uri href, string title = null, string description = null, string method = null)
    {
      return NewVoidAction(context, name, href.AbsoluteUriNullable(), title, description, method);
    }


    public static Control NewVoidAction(this IMasonBuilderContext context, string name, string href, string title = null, string description = null, string method = null)
    {
      if (context.PreferMinimalResponse)
      {
        return new Control(name, href, null, false) { method = method };
      }
      else
      {
        return new Control(name, href, title, false)
        {
          method = method,
          description = description
        };
      }
    }


    public static Control NewJsonAction(this IMasonBuilderContext context, string name, Uri href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = "POST")
    {
      return NewJsonAction(context, name, href.AbsoluteUriNullable(), title, description, schema, schemaUrl, template, method);
    }


    public static Control NewJsonAction(this IMasonBuilderContext context, string name, string href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = "POST")
    {
      if (context.PreferMinimalResponse)
      {
        return new Control(name, href, null)
        {
          method = method,
          template = template,
          serialization = "json"
        };
      }
      else
      {
        return new Control(name, href, title)
        {
          method = method,
          description = description,
          schema = schema,
          schemaUrl = (schemaUrl != null ? schemaUrl.AbsoluteUri : null),
          template = template,
          serialization = "json"
        };
      }
    }


    public static Control NewJsonFilesAction(this IMasonBuilderContext context, string name, Uri href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = "POST")
    {
      return NewJsonFilesAction(context, name, href.AbsoluteUriNullable(), title, description, schema, schemaUrl, template, method);
    }


    public static Control NewJsonFilesAction(this IMasonBuilderContext context, string name, string href, string title = null, string description = null, string schema = null, Uri schemaUrl = null, object template = null, string method = "POST")
    {
      Control c = NewJsonAction(context, name, href, title, description, schema, schemaUrl, template, method);
      c.serialization = "json+files";
      return c;
    }


    public static string AbsoluteUriNullable(this Uri url)
    {
      return url == null ? null : url.AbsoluteUri;
    }
  }
}
