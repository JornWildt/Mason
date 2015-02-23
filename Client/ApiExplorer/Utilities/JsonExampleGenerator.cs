using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Linq;


namespace ApiExplorer.Utilities
{
  public class JsonExampleGenerator
  {
    public string GenerateJsonInstanceFromSchema(JsonSchema schema)
    {
      string result = CreateInstanceFromSchema(schema, 0);
      return result;
    }


    protected string CreateInstanceFromSchema(JsonSchema schema, int indent)
    {
      if (schema.Type == JsonSchemaType.Array)
      {
        var items = schema.Items != null
          ? schema.Items.Select(i => CreateInstanceFromSchema(i, indent+1))
          : Enumerable.Empty<string>();
        return Indent(indent) + "[\n" + string.Join(",\n", items) + Indent(indent) + "]\n";
      }

      if (schema.Type == JsonSchemaType.Object || schema.Type == null)
      {
        var properties = schema.Properties != null
          ? schema.Properties.Select(i => CreateInstanceFromProperty(i, indent+1))
          : Enumerable.Empty<string>();
        return Indent(indent) + "{\n" + string.Join(",\n", properties) + "\n" + Indent(indent) + "}\n";
      }

      if (schema.Type == JsonSchemaType.String)
      {
        return Indent(indent) + "\"\"";
      }

      if (schema.Type == JsonSchemaType.Integer)
      {
        return Indent(indent) + "0";
      }

      if (schema.Type == JsonSchemaType.Boolean)
      {
        return Indent(indent) + "false";
      }

      return "";
    }

    
    private string CreateInstanceFromProperty(KeyValuePair<string, JsonSchema> p, int indent)
    {
      if (p.Value.Type == JsonSchemaType.Object || p.Value.Type == JsonSchemaType.Array)
        return string.Format("{0}\"{1}\":\n{2}", Indent(indent), p.Key, CreateInstanceFromSchema(p.Value, indent+1));
      else
        return string.Format("{0}\"{1}\":{2}", Indent(indent), p.Key, CreateInstanceFromSchema(p.Value, indent + 1));
    }


    private string Indent(int indent)
    {
      string s = "";
      for (int i=0; i<indent; ++i)
      {
        s += "  ";
      }
      return s;
    }
  }
}
