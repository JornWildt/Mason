using Mason.IssueTracker.Server.Origin.Resources;
using Mason.IssueTracker.Server.ServiceIndex.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;
using System.Collections.Generic;


namespace Mason.IssueTracker.Server.Origin.Handlers
{
  public class OriginHandler
  {
    public ICommunicationContext Context { get; set; }


    public object Get()
    {
      dynamic origin = new Resource();
      origin.Title = Settings.OriginName;
      origin.Description = Settings.OriginDescription;

      OriginContactResource c = new OriginContactHandler().Get();
      dynamic contact = new SubResource();
      contact.Address1 = c.Contact.Address1;
      contact.Address2 = c.Contact.Address2;
      contact.PostalCode = c.Contact.PostalCode;
      contact.City = c.Contact.City;
      contact.Phone = c.Contact.Phone;
      contact.Country = c.Contact.Country;

      origin.Contact = contact;

      Uri contactSelfUri = typeof(OriginContactResource).CreateUri();
      Link contactSelfLink = new Link("self", contactSelfUri, "Complete contact information in standard formats such as vCard and jCard");
      contact.AddLink(contactSelfLink);

      Uri logoUri = new Uri(Context.ApplicationBaseUri.EnsureHasTrailingSlash(), "Origins/MinistryOfFun/logo.png");
      Link logoLink = new Link(RelTypes.Logo, logoUri);
      origin.AddLink(logoLink);

      Uri serviceIndexUri = typeof(ServiceIndexResource).CreateUri();
      Link serviceIndexLink = new Link("service", serviceIndexUri); // "service" is registered rel-type
      origin.AddLink(serviceIndexLink);

      return new OriginResource { Value = origin };
    }
  }
}
