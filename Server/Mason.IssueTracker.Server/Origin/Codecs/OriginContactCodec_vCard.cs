using Mason.IssueTracker.Server.Origin.Resources;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System.IO;


namespace Mason.IssueTracker.Server.Origin.Codecs
{
  [MediaType("text/vcard;q=0.5", "vcard")]
  public class OriginContactCodec_vCard : IMediaTypeWriter
  {
    public object Configuration { get; set; }


    public void WriteTo(object resource, IHttpEntity response, string[] parameters)
    {
      if (resource == null)
        return;

      OriginContactResource c = (OriginContactResource)resource;

      using (StreamWriter sw = new StreamWriter(response.Stream))
      {
        sw.WriteLine("BEGIN:VCARD");
        sw.WriteLine("VERSION:4.0");
        sw.WriteLine("FN:" + c.Contact.FullName);
        sw.WriteLine("EMAIL;TYPE=work:" + c.Contact.EMail);
        sw.WriteLine("TEL;TYPE=work;VALUE=text:" + c.Contact.Phone);
        sw.WriteLine("END:VCARD");
      }
    }
  }
}
