using Mason.CaseFile.Server.Origin.Resources;
using Mason.CaseFile.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.Origin.Codecs
{
  public class OriginCodec : MasonCodec<OriginResource>
  {
    protected override Net.Resource ConvertToMason(OriginResource origin)
    {
      Contract.Origin o = new Contract.Origin();

      o.Title = origin.Title;
      o.Introduction = origin.Introduction;

      Uri selfUri = typeof(OriginResource).CreateUri();
      Link selfLink = new Link("self", selfUri);
      o.Links.Add(selfLink);

      return o;
    }
  }
}
