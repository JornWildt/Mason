using Mason.IssueTracker.Server.Origin.Resources;
using Mason.IssueTracker.Server.Utility;


namespace Mason.IssueTracker.Server.Origin.Handlers
{
  public class OriginContactHandler
  {
    public OriginContactResource Get()
    {
      return new OriginContactResource
      {
        Contact = new ContactInformation
        {
          FullName = "Ministry of Fun",
          Address1 = "33 Smiley Road",
          PostalCode = "1234",
          City = "Comediac",
          Phone = "+45 12345678",
          EMail = "info@ministryoffun.org",
          Country = "Denmark"
        }
      };
    }
  }
}
