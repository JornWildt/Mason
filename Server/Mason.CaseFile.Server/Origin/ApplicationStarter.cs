using Mason.CaseFile.Server.CaseFiles.Codecs;
using Mason.CaseFile.Server.CaseFiles.Handlers;
using Mason.CaseFile.Server.Origin.Codecs;
using Mason.CaseFile.Server.Origin.Handlers;
using OpenRasta.Configuration;


namespace Mason.CaseFile.Server.Origin
{
  public static class ApplicationStarter
  {
    public static void Start()
    {
      ResourceSpace.Has.ResourcesOfType<Resources.OriginResource>()
        .AtUri("/origin")
        .HandledBy<OriginHandler>()
        .TranscodedBy<OriginCodec>();

      ResourceSpace.Has.ResourcesOfType<Resources.OriginContactResource>()
        .AtUri("/origin-contact")
        .HandledBy<OriginContactHandler>()
        .TranscodedBy<OriginContactCodec_jCard>()
        .And.TranscodedBy<OriginContactCodec_vCard>()
        .And.TranscodedBy<OriginContactCodec_Mason>();
    }
  }
}
