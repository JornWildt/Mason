using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.Origin.Resources;


namespace Mason.CaseFile.Server.Origin.Codecs
{
  public class OriginContactCodec_jCard : JsonCodec<OriginContactResource>
  {
    protected override object ConvertToJson(OriginContactResource resource)
    {
      var fn = new object[4] { "fn", new { }, "text", "Ministry of Fun" };
      var tel = new object[4] { "tel", new { type = new object[] { "work" } }, "uri", "tel:+45-12345678" };
      var contactProperties = new object[] { fn, tel };
      return new object[]
      {
        "vcard",
        contactProperties
      };
    }
  }
}
