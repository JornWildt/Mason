using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ramone;
using Ramone.MediaTypes;
using System;
using System.IO;


namespace ApiExplorer.Utilities
{
  public class JsonNetCodec : TextCodecBase<JObject>
  {
    protected override JObject ReadFrom(TextReader reader, ReaderContext context)
    {
      using (JsonTextReader jr = new JsonTextReader(reader))
      {
        JObject o = (JObject)JToken.ReadFrom(jr);
        return o;
      }
    }


    protected override void WriteTo(JObject item, TextWriter writer, WriterContext context)
    {
      throw new NotImplementedException();
    }
  }
}
