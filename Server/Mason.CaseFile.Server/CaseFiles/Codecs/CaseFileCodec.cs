using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Origin.Resources;
using Mason.CaseFile.Server.Utility;
using Mason.Net;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
  [MediaType("application/vnd.mason;q=1", "ms")]
  [MediaType("application/json;q=0.5", "json")]
  public class CaseFileCodec : MasonCodec<CaseFileResource>
  {
    protected override Resource ConvertToMason(CaseFileResource casefile)
    {
      Contract.CaseFile c = new Contract.CaseFile();

      c.ID = casefile.ID;

      //c.Links.Add(new Link("self", "/casefiles"));

      Uri selfUri = typeof(CaseFileResource).CreateUri();//new { id = casefile.ID });
      Link selfLink = new Link("self", selfUri);
      c.Links.Add(selfLink);

      //Uri originUri = typeof(OriginResource).CreateUri();
      //Link originLink = new Link("cf:origin", originUri);
      //c.Links.Add(originLink);

      return c;
    }
  }
}
