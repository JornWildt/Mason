using Mason.CaseFile.Server.Utility;


namespace Mason.CaseFile.Server.Origin.Handlers
{
  public class OriginContactHandler
  {
    public ContactInformation Get()
    {
      return new Resources.OriginContactResource
      {
        FullName = "",
        Address1 = "Ministry of Fun",
        Address2 = "33 Laughter Road",
        PostalCode = "1234",
        City = "Comediac",
        Phone = "+45 12345678",
        Country = "Denmark"
      };
    }
  }
}
