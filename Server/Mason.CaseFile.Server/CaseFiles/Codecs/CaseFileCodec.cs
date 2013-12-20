using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Origin.Resources;
using Mason.CaseFile.Server.Utility;
using Mason.Net;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
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
