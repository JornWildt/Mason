using log4net;
using Mason.CaseFile.Server.CaseFiles.Codecs;
using Mason.CaseFile.Server.CaseFiles.Handlers;
using Mason.CaseFile.Server.Domain.CaseFiles;
using Mason.CaseFile.Server.Utility;
using OpenRasta.Configuration;
using OpenRasta.DI;


namespace Mason.CaseFile.Server.CaseFiles
{
  public static class ApplicationStarter
  {
    static ILog Logger = LogManager.GetLogger(typeof(ApplicationStarter));


    public static void Start()
    {
      Logger.Debug("Starting CaseFiles");
      ResourceSpace.Uses.CustomDependency<ICaseFileRepository, CaseFileInMemoryRepository>(DependencyLifetime.Singleton);

      ResourceSpace.Has.ResourcesOfType<Resources.CaseFilesResource>()
        .AtUri("/casefiles")
        .HandledBy<CaseFilesHandler>()
        .TranscodedBy<CaseFilesCodec>();

      ResourceSpace.Has.ResourcesOfType<Resources.CaseFilesQueryResource>()
        .AtUri(UrlPaths.CaseFileQuery)
        .HandledBy<CaseFilesQueryHandler>()
        .TranscodedBy<CaseFilesQueryCodec>();

      ResourceSpace.Has.ResourcesOfType<Resources.CaseFileResource>()
        .AtUri("/casefiles/{number}")
        .HandledBy<CaseFileHandler>()
        .TranscodedBy<CaseFileCodec>();

      LoadDemoData();
    }


    private static void LoadDemoData()
    {
      ICaseFileRepository repo = new CaseFileInMemoryRepository();
      
      Domain.CaseFiles.CaseFile cf = new Domain.CaseFiles.CaseFile("Case file 1", "Blah blah ....");
      repo.Add(cf);
    }
  }
}
