using Mason.CaseFile.Server.CaseFiles.Codecs;
using Mason.CaseFile.Server.CaseFiles.Handlers;
using OpenRasta.Configuration;


namespace Mason.CaseFile.Server.CaseFiles
{
  public static class ApplicationStarter
  {
    public static void Start()
    {
      ResourceSpace.Has.ResourcesOfType<Resources.CaseFilesResource>()
        .AtUri("/casefiles")
        .HandledBy<CaseFilesHandler>()
        .TranscodedBy<CaseFilesCodec>();
      
      ResourceSpace.Has.ResourcesOfType<Resources.CaseFileResource>()
        .AtUri("/casefiles/{id}")
        .HandledBy<CaseFileHandler>()
        .TranscodedBy<CaseFileCodec>();
    }
  }
}
