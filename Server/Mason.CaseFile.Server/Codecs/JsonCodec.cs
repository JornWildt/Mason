using Newtonsoft.Json;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System.IO;


namespace Mason.CaseFile.Server.Codecs
{
  [MediaType("application/json;q=0.5", "json")]
  public abstract class JsonCodec<T> : IMediaTypeWriter
  {
    public object Configuration { get; set; }


    public void WriteTo(object resource, IHttpEntity response, string[] parameters)
    {
      if (resource == null)
        return;

      object representation = ConvertToJson((T)resource);

      JsonSerializer serializer = new JsonSerializer();
      using (StreamWriter sw = new StreamWriter(response.Stream))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        serializer.Serialize(jw, representation);
      }
    }


    protected abstract object ConvertToJson(T resource);
  }
}
