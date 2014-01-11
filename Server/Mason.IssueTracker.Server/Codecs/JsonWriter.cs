using Newtonsoft.Json;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System.IO;


namespace Mason.IssueTracker.Server.Codecs
{
  // Write internal resource of type T as JSON object - with optional conversion to another JSON specific model.
  public class JsonWriter<T> : IMediaTypeWriter
  {
    public object Configuration { get; set; }


    public void WriteTo(object resource, IHttpEntity response, string[] parameters)
    {
      if (resource == null)
        return;

      object representation = ConvertToJsonModel((T)resource);

      JsonSerializer serializer = new JsonSerializer();
      using (StreamWriter sw = new StreamWriter(response.Stream))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        serializer.Serialize(jw, representation);
      }
    }


    protected virtual object ConvertToJsonModel(T resource)
    {
      return resource;
    }
  }
}
