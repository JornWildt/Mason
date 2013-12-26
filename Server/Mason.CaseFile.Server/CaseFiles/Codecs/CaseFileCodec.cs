using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Codecs;
using Mason.Net;
using Newtonsoft.Json;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
  public class CaseFileCodec : CaseFileMasonCodec<CaseFileResource>
  {
    protected override Resource ConvertToCaseFile(CaseFileResource casefile)
    {
      Contract.CaseFile c = new Contract.CaseFile();

      c.ID = casefile.CaseFile.Id.ToString();
      c.Title = casefile.CaseFile.Title;

      //Uri selfUri = typeof(CaseFileResource).CreateUri(new { id = casefile.CaseFile.Id });
      //Link selfLink = new Link("self", selfUri);
      //c.Links.Add(selfLink);

      return c;
    }
  }
}
