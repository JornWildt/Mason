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
          FullName = Settings.OriginName + " (by Jørn Wildt)",
          Address1 = "Mosevej 10B",
          PostalCode = "3450",
          City = "Allerød",
          Phone = "+45 12345678",
          EMail = "jw@fjeldgruppen.dk",
          Country = "Denmark"
        }
      };
    }
  }
}
