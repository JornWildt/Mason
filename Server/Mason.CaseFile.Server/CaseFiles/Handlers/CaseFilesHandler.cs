using System.Collections.Generic;


namespace Mason.CaseFile.Server.CaseFiles.Handlers
{
  public class CaseFilesHandler
  {
    public object Get()
    {
      return new Resources.CaseFilesResource
      {
        CaseFiles = new List<Resources.CaseFileResource>()
        {
        }
      };
    }
  }
}
