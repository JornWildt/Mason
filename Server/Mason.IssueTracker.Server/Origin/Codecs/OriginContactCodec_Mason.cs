using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Origin.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Origin.Codecs
{
  public class OriginContactCodec_Mason : IssueTrackerMasonCodec<OriginContactResource>
  {
    protected override Resource ConvertToIssueTracker(OriginContactResource resource)
    {
      Resource representation = new Resource();

      representation.SetMeta(MasonProperties.MetaProperties.Title, "Contact information for " + Settings.OriginName);
      representation.SetMeta(MasonProperties.MetaProperties.Description, "This resource contains the contact information for " + Settings.OriginName + ". Use either content negotiation or links for different versions");

      dynamic c = representation;
      c.Address1 = resource.Contact.Address1;
      c.Address2 = resource.Contact.Address2;
      c.PostalCode = resource.Contact.PostalCode;
      c.City = resource.Contact.City;
      c.EMail = resource.Contact.EMail;
      c.Phone = resource.Contact.Phone;
      c.Country = resource.Contact.Country;

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
