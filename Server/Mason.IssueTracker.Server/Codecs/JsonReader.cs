using Newtonsoft.Json;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web;
using System.IO;


namespace Mason.IssueTracker.Server.Codecs
{
  [MediaType("application/json")]
  public class JsonReader<T> : IMediaTypeReader
  {
    public object Configuration { get; set; }


    public object ReadFrom(IHttpEntity request, IType destinationType, string destinationName)
    {
      JsonSerializer serializer = new JsonSerializer();
      using (StreamReader sr = new StreamReader(request.Stream))
      using (JsonReader jr = new JsonTextReader(sr))
      {
        return serializer.Deserialize<T>(jr);
      }
    }
  }
}
