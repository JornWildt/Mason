using Mason.CaseFile.Server.Codecs;
using Mason.CaseFile.Server.ServiceIndex.Resources;


namespace Mason.CaseFile.Server.ServiceIndex.Codecs
{
  public class ServiceIndexCodec : CaseFileMasonCodec<ServiceIndexResource>
  {
    protected override Net.Resource ConvertToCaseFile(ServiceIndexResource ServiceIndex)
    {
      Contract.ServiceIndex s = new Contract.ServiceIndex();

      s.Title = ServiceIndex.Title;
      s.Description = ServiceIndex.Description;

      return s;
    }
  }
}
