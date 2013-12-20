using Mason.CaseFile.Server.CaseFiles.Codecs;
using Mason.CaseFile.Server.CaseFiles.Handlers;
using OpenRasta.Configuration;


namespace Mason.CaseFile.Server.CaseFiles
{
  public static class ApplicationStarter
  {
    public static void Start()
    {
      ResourceSpace.Has.ResourcesOfType<Resources.CaseFileResource>()
        .AtUri("/casefiles")
        .HandledBy<CaseFileHandler>()
        .TranscodedBy<CaseFileCodec>();
    }
  }
}
