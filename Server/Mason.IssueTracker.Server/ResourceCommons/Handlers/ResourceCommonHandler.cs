using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.ResourceCommons.Resources;
using Mason.IssueTracker.Server.ServiceIndex.Resources;
using Mason.IssueTracker.Server.Utility;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.IssueTracker.Server.ResourceCommons.Handlers
{
  public class ResourceCommonHandler
  {
    public ICommunicationContext Context { get; set; }


    public object Get()
    {
      dynamic common = new Resource();
      common.Title = Settings.OriginName;
      common.Description = Settings.OriginDescription;

      Uri contactUri = typeof(ContactResource).CreateUri();
      Link contactLink = new Link(RelTypes.Contact, contactUri, "Complete contact information in standard formats such as vCard and jCard");
      common.AddLink(contactLink);

      Uri logoUri = new Uri(Context.ApplicationBaseUri.EnsureHasTrailingSlash(), "Origins/MinistryOfFun/logo.png");
      Link logoLink = new Link(RelTypes.Logo, logoUri);
      common.AddLink(logoLink);

      Uri serviceIndexUri = typeof(ServiceIndexResource).CreateUri();
      Link serviceIndexLink = new Link("service", serviceIndexUri);
      common.AddLink(serviceIndexLink);

      Uri selfUri = typeof(ResourceCommonResource).CreateUri();
      Link selfLink = new Link("self", selfUri);
      common.AddLink(selfLink);

      return new ResourceCommonResource { Value = common };
    }
  }
}
