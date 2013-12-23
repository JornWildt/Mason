using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.ServiceIndex.Resources;


namespace Mason.CaseFile.Server.ServiceIndex.Codecs
{
  public class ServiceIndexCodec : MasonCodec<ServiceIndexResource>
  {
    protected override Net.Resource ConvertToMason(ServiceIndexResource ServiceIndex)
    {
      Contract.ServiceIndex s = new Contract.ServiceIndex();

      s.Title = ServiceIndex.Title;
      s.Description = ServiceIndex.Description;

      //Uri selfUri = typeof(ServiceIndexResource).CreateUri();
      //Link selfLink = new Link("self", selfUri);
      //s.Links.Add(selfLink);

      return s;
    }
  }
}
