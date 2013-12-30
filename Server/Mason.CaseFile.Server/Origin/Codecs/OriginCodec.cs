using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.Origin.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.Origin.Codecs
{
  public class OriginCodec : CaseFileMasonCodec<OriginResource>
  {
    protected override Net.Resource ConvertToCaseFile(OriginResource resource)
    {
      Contract.Origin o = resource.Value;

      o.SetMeta(MasonProperties.Title, resource.Value.Title);
      o.SetMeta(MasonProperties.Description, resource.Value.Description);

      Uri selfUri = typeof(OriginResource).CreateUri();
      Link selfLink = new Link("self", selfUri);
      o.AddLink(selfLink);

      return o;
    }
  }
}
