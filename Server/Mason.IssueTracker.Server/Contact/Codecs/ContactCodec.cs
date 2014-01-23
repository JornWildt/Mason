using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Contact.Codecs
{
  public class ContactCodec : IssueTrackerMasonCodec<ContactResource>
  {
    protected override Resource ConvertToIssueTracker(ContactResource resource)
    {
      Resource representation = new Resource();

      if (!CommunicationContext.PreferMinimalResponse())
      {
        representation.SetMeta(MasonProperties.MetaProperties.Title, "Contact information for " + Settings.OriginName);
        representation.SetMeta(MasonProperties.MetaProperties.Description, "This resource contains the contact information for " + Settings.OriginName + ". Use either content negotiation or links for different formats.");
      }

      dynamic c = representation;
      c.Name = resource.Contact.FullName;
      c.Address1 = resource.Contact.Address1;
      c.Address2 = resource.Contact.Address2;
      c.PostalCode = resource.Contact.PostalCode;
      c.City = resource.Contact.City;
      c.EMail = resource.Contact.EMail;
      c.Phone = resource.Contact.Phone;
      c.Country = resource.Contact.Country;

      string cardBaseUrl = typeof(ContactResource).CreateUri().AbsoluteUri;

      Link selfLink = CommunicationContext.NewLink("self", cardBaseUrl, "Default contact information");
      c.AddLink(selfLink);

      Uri vCardUri = new Uri(cardBaseUrl + ".vcard");
      Link vCardLink = CommunicationContext.NewLink("alternate", vCardUri, "Contact information as vCard", "text/vcard");
      c.AddLink(vCardLink);

      Uri jCardUri = new Uri(cardBaseUrl + ".jcard");
      Link jCardLink = CommunicationContext.NewLink("alternate", jCardUri, "Contact information as jCard", "application/json");
      vCardLink.AddAltLink(jCardLink);

      return c;
    }
  }
}
