using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.Origin.Resources;
using Mason.Net;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.Origin.Codecs
{
  public class OriginContactCodec_Mason : CaseFileMasonCodec<OriginContactResource>
  {
    protected override Net.Resource ConvertToCaseFile(OriginContactResource resource)
    {
      Net.Resource representation = new Net.Resource();

      dynamic c = representation;
      c.Address1 = resource.Address1;
      c.Address2 = resource.Address2;
      c.PostalCode = resource.PostalCode;
      c.City = resource.City;
      c.EMail = resource.EMail;
      c.Phone = resource.Phone;
      c.Country = resource.Country;

      string cardBaseUrl = typeof(OriginContactResource).CreateUri().AbsoluteUri;

      Uri jCardUri = new Uri(cardBaseUrl + ".jcard");
      Link jCardLink = new Link("alternate", jCardUri, "Contact information as jCard", "application/json");
      c.AddLink(jCardLink);

      Uri vCardUri = new Uri(cardBaseUrl + ".vcard");
      Link vCardLink = new Link("alternate", vCardUri, "Contact information as vCard", "text/vcard");
      c.AddLink(vCardLink);

      return c;
    }
  }
}
