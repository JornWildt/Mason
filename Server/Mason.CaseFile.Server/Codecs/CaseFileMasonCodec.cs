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
      r.AddLink(originLink);

      r.AddNamespace(new Net.Namespace(RelTypes.NamespaceAlias, RelTypes.Namespace));
      r["mason:profile"] = Profiles.CaseFile;

      return r;
    }


    protected abstract Mason.Net.Resource ConvertToCaseFile(T resource);
  }
}
