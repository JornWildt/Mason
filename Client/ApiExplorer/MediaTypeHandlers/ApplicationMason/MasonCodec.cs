using Mason.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Ramone;
using Ramone.MediaTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonCodec : TextCodecBase<Resource>
  {
    //class x : CustomCreationConverter<JsonObjectAttribute>
    //{
    //  public override dynamic Create(Type objectType)
    //  {
    //    return (dynamic)Activator.CreateInstance(objectType);
    //    //return new SubResource();
    //  }
    //}


    protected override Resource ReadFrom(TextReader reader, ReaderContext context)
    {
      JsonSerializer serializer = new JsonSerializer();
      //serializer.Converters.Add(new x());
      using (JsonTextReader jr = new JsonTextReader(reader))
      {
        object result = serializer.Deserialize(jr);
        Resource resource = ConvertToResource(result);
        return resource;
      }
    }

    
    private Resource ConvertToResource(object result)
    {
      JObject o = result as JObject;
      if (o != null)
      {
        Resource resource = new Resource();
        foreach (var pair in o)
        {
          if (pair.Key == "mason:links")
          {
            resource.Links = new List<Link>(
              pair.Value.Children().Select(t => new Link(GetValue<string>(t, "rel"), GetValue<string>(t, "href"), GetValue<string>(t, "title"), GetValue<string>(t, "type"))));
          }
          else
          {
            resource[pair.Key] = pair.Value;
          }
        }
        return resource;
      }
      return new Resource();
    }


    protected T GetValue<T>(JToken t, string name)
      where T : class
    {
      JToken value = t[name];
      if (value != null)
        return value.Value<T>();
      return null;
    }


    protected override void WriteTo(Resource item, TextWriter writer, WriterContext context)
    {
      throw new NotImplementedException();
    }
  }
}
