using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.Utility;


namespace Mason.IssueTracker.Server.Contact.Handlers
{
  public class ContactHandler
  {
    public ContactResource Get()
    {
      return new ContactResource
      {
        Contact = new ContactInformation
        {
          FullName = Settings.OriginName,
          Address1 = "33 Demo Road",
          PostalCode = "1234",
          City = "Demon",
          Phone = "+45 12345678",
          EMail = "info@demostradius.org",
          Country = "Denmark"
        }
      };
    }
  }
}
