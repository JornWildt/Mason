using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Codecs;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
  public class CaseFilesQueryCodec : CaseFileMasonCodec<CaseFilesQueryResource>
  {
    protected override Net.Resource ConvertToCaseFile(CaseFilesQueryResource resource)
    {
      Contract.CaseFileCollection cc = new Contract.CaseFileCollection();

      foreach (CaseFileResource c in resource.CaseFiles)
      {
        Contract.CaseFileCollectionItem item = new Contract.CaseFileCollectionItem();
        item.ID = c.CaseFile.Id.ToString();
        item.Title = c.CaseFile.Title;

        Uri itemSelfUri = typeof(CaseFileResource).CreateUri(new { id = c.CaseFile.Id });
        Link itemSelfLink = new Link("self", itemSelfUri);
        item.Links.Add(itemSelfLink);

        cc.CaseFiles.Add(item);
      }

      Uri selfUri = typeof(CaseFilesResource).CreateUri();
      Link selfLink = new Link("self", selfUri);
      cc.Links.Add(selfLink);

      return cc;
    }
  }
}
