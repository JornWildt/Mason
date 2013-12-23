using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Utility;
using Mason.Net;
using Newtonsoft.Json;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
  [JsonObject(MemberSerialization.OptOut)]
  public class CaseFileCodec : MasonCodec<CaseFileResource>
  {
    protected override Resource ConvertToMason(CaseFileResource casefile)
    {
      Contract.CaseFile c = new Contract.CaseFile();

      c.ID = casefile.ID;

      Uri selfUri = typeof(CaseFileResource).CreateUri(new { id = casefile.ID });
      Link selfLink = new Link("self", selfUri);
      c.Links.Add(selfLink);

      //Uri originUri = typeof(OriginResource).CreateUri();
      //Link originLink = new Link("cf:origin", originUri);
      //c.Links.Add(originLink);

      return c;
    }
  }
}
