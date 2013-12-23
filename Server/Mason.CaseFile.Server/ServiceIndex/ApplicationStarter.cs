using Mason.CaseFile.Server.CaseFiles.Codecs;
using Mason.CaseFile.Server.CaseFiles.Handlers;
using Mason.CaseFile.Server.ServiceIndex.Codecs;
using Mason.CaseFile.Server.ServiceIndex.Handlers;
using OpenRasta.Configuration;


namespace Mason.CaseFile.Server.ServiceIndex
{
  public static class ApplicationStarter
  {
    public static void Start()
    {
      ResourceSpace.Has.ResourcesOfType<Resources.ServiceIndexResource>()
        .AtUri("/service-index")
        .HandledBy<ServiceIndexHandler>()
        .TranscodedBy<ServiceIndexCodec>();
    }
  }
}
