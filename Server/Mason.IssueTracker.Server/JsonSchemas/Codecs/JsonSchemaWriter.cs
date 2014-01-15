using Mason.IssueTracker.Server.JsonSchemas.Resources;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;


namespace Mason.IssueTracker.Server.JsonSchemas.Codecs
{
  //[MediaType("application/schema+json;q=1")]
  [MediaType("application/json;q=0.9", ".json")]
  [MediaType("text/plain;q=0.1", ".txt")]
  public class JsonSchemaWriter : IMediaTypeWriter
  {
    public object Configuration { get; set; }

    
    #region IMediaTypeWriter Members

    public void WriteTo(object entity, IHttpEntity response, string[] codecParameters)
    {
      SchemaTypeResource tr = entity as SchemaTypeResource;
      if (tr == null)
        throw new InvalidOperationException("Expected Type in JSON schema writer");

      Type t = tr.SchemaType;

      using (StreamWriter sw = new StreamWriter(response.Stream))
      {
        sw.WriteLine("{");
        sw.WriteLine("title: \"Schema\",");
        sw.WriteLine("type: \"object\",");
        sw.WriteLine("properties: {");
        Write(sw, t);
        sw.WriteLine("\n}");
        sw.WriteLine("\n}");
      }
    }

    #endregion


    private void Write(StreamWriter sw, Type t)
    {
      bool first = true;
      // So far very very simpel
      foreach (PropertyInfo p in t.GetProperties())
      {
        if (!first)
          sw.WriteLine(",");
        sw.Write(string.Format("\"{0}\": {{ type: \"{1}\" }}", p.Name, GetSchemaType(p.PropertyType)));
        first = false;
      }
    }


    private string GetSchemaType(Type t)
    {
      if (t == typeof(bool))
        return "boolean";
      if (t == typeof(int) || t == typeof(long) || t == typeof(uint) || t == typeof(ulong))
        return "integer";
      if (t == typeof(decimal) || t == typeof(float) || t == typeof(double))
        return "number";
      if (t == typeof(string))
        return "string";
      if (typeof(IDictionary).IsAssignableFrom(t) || typeof(NameValueCollection).IsAssignableFrom(t))
        return "object";
      if (t.IsArray || typeof(IList).IsAssignableFrom(t))
        return "array";
      if (t.IsClass)
        return "object";

      throw new InvalidOperationException(string.Format("Unhandled type '{0}'", t));
    }
  }
}
