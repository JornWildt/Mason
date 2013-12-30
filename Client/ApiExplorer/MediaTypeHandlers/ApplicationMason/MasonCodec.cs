using Mason.Net;
using Newtonsoft.Json;
using Ramone;
using Ramone.MediaTypes;
using System;
using System.IO;


namespace ApiExplorer.MediaTypeHandlers.ApplicationMason
{
  public class MasonCodec : TextCodecBase<Resource>
  {
    protected override Resource ReadFrom(TextReader reader, ReaderContext context)
    {
      JsonSerializer serializer = new JsonSerializer();
      using (JsonTextReader jr = new JsonTextReader(reader))
      {
        object result = serializer.Deserialize<Resource>(jr);
        return (Resource)result;
      }
    }


    protected override void WriteTo(Resource item, TextWriter writer, WriterContext context)
    {
      throw new NotImplementedException();
    }
  }
}
