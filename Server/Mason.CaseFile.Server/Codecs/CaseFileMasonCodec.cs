using Mason.CaseFile.Server.Origin.Resources;
using Mason.Net;
using OpenRasta.Web;
using System;


namespace Mason.CaseFile.Server.Codecs
{
  public abstract class CaseFileMasonCodec<T> : MasonCodec<T>
  {
    protected override Net.Resource ConvertToMason(T resource)
    {
      Net.Resource r = ConvertToCaseFile(resource);

      Uri originUri = typeof(OriginResource).CreateUri();
      Link originLink = new Link("cf:origin", originUri);
      r.Links.Add(originLink);

      r.Namespaces.Add(new Net.Namespace("cf", "http://mason-casefile.dk/rels/"));

      return r;
    }


    protected abstract Mason.Net.Resource ConvertToCaseFile(T resource);
  }
}
