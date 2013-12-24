﻿using Mason.CaseFile.Server.Origin.Resources;
using OpenRasta.Codecs;
using OpenRasta.Web;
using System.IO;


namespace Mason.CaseFile.Server.Origin.Codecs
{
  [MediaType("text/vcard;q=1", "vcard")]
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
        sw.WriteLine("FN:" + c.FullName);
        //sw.WriteLine("");
        sw.WriteLine("END:VCARD");
      }
    }
  }
}
