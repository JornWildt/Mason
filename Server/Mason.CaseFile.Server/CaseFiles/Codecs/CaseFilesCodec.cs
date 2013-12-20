using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
  public class CaseFilesCodec : MasonCodec<CaseFilesResource>
  {
    protected override Net.Resource ConvertToMason(CaseFilesResource resource)
    {
      Contract.CaseFileCollection cc = new Contract.CaseFileCollection();

      foreach (CaseFileResource c in resource.CaseFiles)
      {
        Contract.CaseFileCollectionItem item = new Contract.CaseFileCollectionItem();
        item.ID = c.ID;
        item.Title = c.Title;

        Uri itemSelfUri = typeof(CaseFileResource).CreateUri(new { id = c.ID });
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
