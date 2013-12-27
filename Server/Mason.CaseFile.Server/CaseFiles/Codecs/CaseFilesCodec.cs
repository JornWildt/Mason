using Mason.CaseFile.Server.CaseFiles.Resources;
using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.Domain.CaseFiles;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.CaseFiles.Codecs
{
  public class CaseFilesCodec : CaseFileMasonCodec<CaseFilesResource>
  {
    protected override Net.Resource ConvertToCaseFile(CaseFilesResource resource)
    {
      Contract.CaseFileCollection cc = new Contract.CaseFileCollection();

      foreach (CaseFileListItem c in resource.CaseFiles)
      {
        Contract.CaseFileCollectionItem item = new Contract.CaseFileCollectionItem();
        item.ID = c.Id.ToString();
        item.Title = c.Title;

        Uri itemSelfUri = typeof(CaseFileResource).CreateUri(new { id = c.Id });
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
