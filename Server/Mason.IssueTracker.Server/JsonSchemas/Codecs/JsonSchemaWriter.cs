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
  [MediaType("application/json;q=0.9", ".json")]
  [MediaType("application/schema+json;q=0.8")]
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
        WriteSchema(sw, t);
      }
    }

    #endregion


    private void WriteSchema(StreamWriter sw, Type t)
    {
      sw.WriteLine("{");
      sw.WriteLine("\"title\": \"Schema\",");
      sw.WriteLine("\"type\": \"object\",");
      sw.WriteLine("\"properties\": {");
      WriteProperties(sw, t);
      sw.WriteLine("\n}");
      sw.WriteLine("\n}");
    }


    private void WriteProperties(StreamWriter sw, Type t)
    {
      bool first = true;
      // So far very very simpel
      foreach (PropertyInfo p in t.GetProperties())
      {
        if (!first)
          sw.WriteLine(",");
        string schemaType = GetSchemaType(p.PropertyType);
        sw.Write(string.Format("\"{0}\": {{ \"type\": \"{1}\"", p.Name, schemaType));
        if (schemaType == "object" && t.IsClass)
        {
          sw.WriteLine(",\n   \"properties\": {");
          WriteProperties(sw, p.PropertyType);
          sw.WriteLine("}");
        }
        sw.Write("}");
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
