using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.Origin.Handlers;
using Mason.CaseFile.Server.Origin.Resources;
using Mason.CaseFile.Server.Utility;


namespace Mason.CaseFile.Server.Origin.Codecs
{
  public class OriginContactCodec_jCard : JsonCodec<OriginContactResource>
  {
    protected override object ConvertToJson(OriginContactResource resource)
    {
      ContactInformation c = new OriginContactHandler().Get();

      var fn = new object[4] { "fn", new { }, "text", c.FullName };
      var tel = new object[4] { "tel", new { type = new object[] { "work" } }, "uri", "tel:" + c.Phone };
      var contactProperties = new object[] { fn, tel };
      return new object[]
      {
        "vcard",
        contactProperties
      };
    }
  }
}
