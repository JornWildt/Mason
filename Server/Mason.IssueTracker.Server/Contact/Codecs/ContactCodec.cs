using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.Utility;
using MasonBuilder.Net;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.Contact.Codecs
{
  public class ContactCodec : IssueTrackerMasonCodec<ContactResource>
  {
    protected override Resource ConvertToIssueTracker(ContactResource resource)
    {
      Resource contact = new Resource();

      if (!MasonBuilderContext.PreferMinimalResponse)
      {
        contact.Meta.Title = "Contact information for " + Settings.OriginName;
        contact.Meta.Description = "This resource contains the contact information for " + Settings.OriginName + ". Use either content negotiation or links for different formats.";
      }

      dynamic c = contact;
      c.Name = resource.Contact.FullName;
      c.Address1 = resource.Contact.Address1;
      c.Address2 = resource.Contact.Address2;
      c.PostalCode = resource.Contact.PostalCode;
      c.City = resource.Contact.City;
      c.EMail = resource.Contact.EMail;
      c.Phone = resource.Contact.Phone;
      c.Country = resource.Contact.Country;

      string cardBaseUrl = typeof(ContactResource).CreateUri().AbsoluteUri;

      Link selfLink = MasonBuilderContext.NewLink("self", cardBaseUrl);
      contact.AddNavigation(selfLink);

      Uri vCardUri = new Uri(cardBaseUrl + ".vcard");
      Link vCardLink = MasonBuilderContext.NewLink("alternate", vCardUri, "Contact information as vCard", "text/vcard");
      contact.AddNavigation(vCardLink);

      Uri jCardUri = new Uri(cardBaseUrl + ".jcard");
      Link jCardLink = MasonBuilderContext.NewLink("alternate", jCardUri, "Contact information as jCard", "application/json");
      vCardLink.AddAlternateLink(jCardLink);

      return c;
    }
  }
}
