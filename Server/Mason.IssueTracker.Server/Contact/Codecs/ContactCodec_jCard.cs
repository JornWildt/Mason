using Mason.IssueTracker.Server.Codecs;
using Mason.IssueTracker.Server.Contact.Handlers;
using Mason.IssueTracker.Server.Contact.Resources;
using Mason.IssueTracker.Server.Utility;
using OpenRasta.Codecs;


namespace Mason.IssueTracker.Server.Contact.Codecs
{
  [MediaType("application/json;q=0.9", "jcard")]
  public class ContactCodec_jCard : JsonWriter<ContactResource>
  {
    protected override object ConvertToJsonModel(ContactResource resource)
    {
      ContactResource c = new ContactHandler().Get();

      var fn = new object[4] { "fn", new { }, "text", c.Contact.FullName };
      var tel = new object[4] { "tel", new { type = new object[] { "work" } }, "text", "" + c.Contact.Phone };
      var contactProperties = new object[] { fn, tel };
      return new object[]
      {
        "vcard",
        contactProperties
      };
    }
  }
}
